﻿var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

var sendBtn = $("#sendButton");
sendBtn.prop("disabled", true);

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    li.textContent = message;
    li.setAttribute("class", "list-group-item");
    var list = document.getElementById("messagesContainer");
    list.insertBefore(li, list.firtChild);
});

connection.start().then(sendBtn.prop("disabled", false)).catch(function (err) {
    toastr.error(err.toString(), "Error");;
});

$("#sendButton").click(function () {
    let newText = $("#newMessageBox").val();
    $("#newMessageBox").val("");
    connection.invoke("SendMessage", "Gino", newText).catch(function (err) {
        return console.error(err.toString());
    });
});