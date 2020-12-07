"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .withAutomaticReconnect()                                 //自动重连
    .build();


//设置发送按钮可用
document.getElementById("sendButton").disabled = true;


//设置客户端监听 ReceiveMessage方法
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

//启动并设置按钮不可用
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//给按钮添加事件
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(error=> {
        console.error(error.toString());
    });
    event.preventDefault();
});

//设置关闭长连接后，输出错误并重连
connection.onclose(async (error) => {
    console.error(error.toString());
    //调用connection.start
    await start();
});


async function start() {
    try {
        await connection.start();
        //连接成功处理
        //打印日志
        console.info("start success");

        document.getElementById("sendButton").disabled = false;

    } catch (err) {
        //连接失败处理、打印日志
        console.error(err.toString());
    }
};
