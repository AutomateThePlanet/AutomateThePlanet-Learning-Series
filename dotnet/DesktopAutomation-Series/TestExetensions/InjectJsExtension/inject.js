(function() {

	// just place a div at top right
	var div = document.createElement('div');
	div.style.position = 'fixed';
	div.style.top = 0;
	div.style.right = 0;
	div.textContent = 'Injected!';
	document.body.appendChild(div);
	window.myVar = 1;
	alert('inserted self... giggity');

})();