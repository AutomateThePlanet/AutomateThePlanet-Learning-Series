// //  chrome.extension.sendMessage({name: 'screenshot'}, function(response) {
// //         alert("Sends the Message");
// //     });

chrome.runtime.sendMessage({greeting: "screenshot"}, function(response) {
//   alert("Message Sent");
});