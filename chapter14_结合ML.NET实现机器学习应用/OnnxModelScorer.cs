using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using ONNX_Sample.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ONNX_Sample
{
    class OnnxModelScorer
    {
        private readonly string imagesFolder;
        private readonly string modelLocation;
        private readonly MLContext mlContext;


        public OnnxModelScorer(string imagesFolder, string modelLocation, MLContext mlContext)
        {
            this.imagesFolder = imagesFolder;
            this.modelLocation = modelLocation;
            this.mlContext = mlContext;
        }

        public struct ImageNetSettings
        {
            public const int imageHeight = 224;
            public const int imageWidth = 224;
            public const float Mean = 127;
            public const float Scale = 1;
            public const bool ChannelsLast = false;
        }

        public struct ImageNetModelSettings
        {
            // input tensor name
            public const string ModelInput = "data";

            // output tensor name
            public const string ModelOutput = "resnetv17_dense0_fwd";
        }

        private ITransformer LoadModel(string modelLocation)
        {
            Console.WriteLine("Read model");
            Console.WriteLine($"Model location: {modelLocation}");
            Console.WriteLine($"Default parameters: image size=({ImageNetSettings.imageWidth},{ImageNetSettings.imageHeight})");

            // Create IDataView from empty list to obtain input data schema
            var data = mlContext.Data.LoadFromEnumerable(new List<ImageNetData>());

            // Define scoring pipeline
            var pipeline = mlContext.Transforms.LoadImages(outputColumnName: ImageNetModelSettings.ModelInput, 
                                                           imageFolder: "", 
                                                           inputColumnName: nameof(ImageNetData.ImagePath))
                            .Append(mlContext.Transforms.ResizeImages(outputColumnName: ImageNetModelSettings.ModelInput,
                                                                        imageWidth: ImageNetSettings.imageWidth,
                                                                        imageHeight: ImageNetSettings.imageHeight,
                                                                        inputColumnName: ImageNetModelSettings.ModelInput,
                                                                        resizing: ImageResizingEstimator.ResizingKind.IsoCrop,
                                                                        cropAnchor: ImageResizingEstimator.Anchor.Center))
                            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: ImageNetModelSettings.ModelInput, 
                                                                        interleavePixelColors: ImageNetSettings.ChannelsLast))
                            .Append(mlContext.Transforms.NormalizeGlobalContrast(outputColumnName: ImageNetModelSettings.ModelInput,
                                                                                    inputColumnName: ImageNetModelSettings.ModelInput,
                                                                                    ensureZeroMean: true,
                                                                                    ensureUnitStandardDeviation: true,
                                                                                    scale: ImageNetSettings.Scale))
                            .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, 
                                                                        outputColumnNames: new[] { ImageNetModelSettings.ModelOutput }, 
                                                                        inputColumnNames: new[] { ImageNetModelSettings.ModelInput }));

            // Fit scoring pipeline
            var model = pipeline.Fit(data);

            return model;
        }

        private IEnumerable<float[]> PredictDataUsingModel(IDataView testData, ITransformer model)
        {
            Console.WriteLine($"Images location: {imagesFolder}");
            Console.WriteLine("");
            Console.WriteLine("=====Identify the objects in the images=====");
            Console.WriteLine("");

            IDataView scoredData = model.Transform(testData);

            IEnumerable<float[]> probabilities = scoredData.GetColumn<float[]>(ImageNetModelSettings.ModelOutput);

            return probabilities;
        }

        public IEnumerable<float[]> Score(IDataView data)
        {
            var model = LoadModel(modelLocation);

            return PredictDataUsingModel(data, model);
        }
    }
}
