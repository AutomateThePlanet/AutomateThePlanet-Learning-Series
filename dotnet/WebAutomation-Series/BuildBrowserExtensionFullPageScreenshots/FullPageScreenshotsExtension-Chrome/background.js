chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
		if (request.greeting == "screenshot")
		{
			 chrome.tabs.query(
        				{ currentWindow: true, active: true },
        				function (tabArray) {
							chrome.tabs.executeScript(tabArray[0].id, { file: 'html2canvas.js'});
						}
			);
		}
	}     
  );