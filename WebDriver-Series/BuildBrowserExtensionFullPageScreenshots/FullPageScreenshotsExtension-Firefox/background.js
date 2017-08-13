browser.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
		if (request.greeting == "screenshot")
		{
			 console.log("test");
			 browser.tabs.query(
        				{ currentWindow: true, active: true },
        				function (tabArray) {
							//browser.tabs.executeScript(tabArray[0].id, { file: 'inject.js'});
							browser.tabs.executeScript(tabArray[0].id, { file: 'html2canvas.js'});
						}
			);
		}
	}     
  );
  
//   browser.browserAction.onClicked.addListener(function (tab) {
// 	// for the current tab, inject the "inject.js" file & execute it
// 	chrome.tabs.executeScript(tab.ib, {
// 		file: 'html2canvas.js'
// 	});
// });