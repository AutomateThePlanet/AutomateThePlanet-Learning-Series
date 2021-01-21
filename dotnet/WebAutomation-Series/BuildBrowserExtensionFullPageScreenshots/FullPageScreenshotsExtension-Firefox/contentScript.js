browser.runtime.sendMessage({greeting: "screenshot"}, function(response) {
  alert("Message Sent");
});