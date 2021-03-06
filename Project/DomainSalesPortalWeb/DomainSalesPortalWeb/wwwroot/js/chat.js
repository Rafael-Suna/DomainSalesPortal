﻿//"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44325/domainHubs").build();


//console.log(connection);

//    connection.on("ReceiveDomains", function (data) {

//        debugger
//        alert(data);
//        console.log(data);
//        //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//        //var encodedMsg = user + " says " + msg;
//        //var li = document.createElement("li");
//        //li.textContent = encodedMsg;
//        //document.getElementById("messagesList").appendChild(li);
//    });

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/domainHubs")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 1000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();



connection.on("ReceiveDomains", function (data) {
   
  
    var count = 0;

    $.each(data, function (index, value) {
      

        var id = value.id;
        var trClass = value.class;


        if (trClass!="") {

            count = count+1

        }

      
   

        $("#" + id + "").addClass(trClass);

 

    });



    var element = document.getElementById("badgeSpan");
    element.textContent = count;
       
  
        console.log(data);
        //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        //var encodedMsg = user + " says " + msg;
        //var li = document.createElement("li");
        //li.textContent = encodedMsg;
        //document.getElementById("messagesList").appendChild(li);
  });