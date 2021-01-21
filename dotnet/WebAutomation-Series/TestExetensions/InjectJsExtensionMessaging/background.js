// this is the background code...

// listen for our browerAction to be clicked

// chrome.browserAction.onClicked.addListener(function (tab) {
// 	// for the current tab, inject the "inject.js" file & execute it
// 	chrome.tabs.executeScript(tab.ib, {
// 		file: 'html2canvas.js'
// 	});
// 	chrome.tabs.executeScript(tab.ib, {
// 		file: 'inject.js'
// 	});
// });

// chrome.extension.onMessage.addListener(function(request, sender, sendResponse) {
//     if (request.name == 'screenshot') {
//         // chrome.tabs.executeScript(tab.ib, {
// 		// 	file: 'html2canvas.js'
// 		// });
// 		alert("message received" + request.name);
// 		chrome.tabs.executeScript(tab.ib, {
// 			file: 'inject.js'
// 		});
//     }
//     return true;
// });


chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
		if (request.greeting == "screenshot")
		{
			
			// sender.tab.executeScript({
			// 	file: 'inject.js'
			// });
			// var currentTab = chrome.tabs.getSelected(null);
			// currentTab.executeScript(null, { file: 'inject.js'});

			 chrome.tabs.query(
        				{ currentWindow: true, active: true },
        				function (tabArray) {
							chrome.tabs.executeScript(tabArray[0].id, { file: 'html2canvas.js'});
						}
			);
		}
	}     
  );