var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var id = $("#chatroomId").val();
var userName = $("#userName").val();

var sendBtn = $("#sendButton");
sendBtn.prop("disabled", true);

connection.on(id, function (user, timestamp, message) {
    var li = document.createElement("li");
    if (user === userName) {
        li.setAttribute("class", "list-group-item");
    }
    else {
        li.setAttribute("class", "list-group-item list-group-item-primary");
    }
    li.innerHTML = "<div class='container'><p><span class='font-weight-bold'>" + user + ":</span> " + message + "</p><span class='float-left timestamp'>" + timestamp + "</span></div>"
    var list = document.getElementById("messagesContainer");
    list.insertBefore(li, list.childNodes[0]);
});

connection.start().then(sendBtn.prop("disabled", false)).catch(function (err) {
    toastr.error(err.toString(), "Error");;
});

$("#sendButton").click(function () {
    let newText = $("#newMessageBox").val();
    $("#newMessageBox").val(""); 
    let userId = $("#userId").val();
    connection.invoke("SendMessage", id, userId, newText).catch(function (err) {
        return console.error(err.toString());
    });
});