if (typeof Windows !== 'undefined' &&
    typeof Windows.UI !== 'undefined' &&
    typeof Windows.ApplicationModel !== 'undefined') 
  {
  // Subscribe to the Windows Activation Event
  Windows.UI.WebUI.WebUIApplication.addEventListener("activated", function (args) {
    var activation = Windows.ApplicationModel.Activation;
    // Check to see if the app was activated by a voice command
    if (args.kind === activation.ActivationKind.voiceCommand) {
      // Get the speech reco
      var speechRecognitionResult = args.result;
      var textSpoken = speechRecognitionResult.text;
      // Determine the command type {search} defined in vcd
      if (speechRecognitionResult.rulePath[0] === "search") {
        // Determine the stream name specified
        if (textSpoken.includes('foo') || textSpoken.includes('Foo')) {
          console.log("The user is searching for foo");
        }
        else if (textSpoken.includes('bar') || textSpoken.includes('Bar') ) {
          console.log("The user is searching for a bar");
        }
        else {
          console.log("Invalid search term specified by user");
        }
      }
      else { 
        console.log("No valid command specified");
      }
    }
  });
} else {
  console.log("Windows namespace is unavaiable");
}