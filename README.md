This is an image upload application.

I started out by doing some UML diagrams to get a better idea of the flow that I wanted.

I then made the basic setup, creating the Application project and the ImageVerification project, going for a vertical (feature) slice architecture (although horizontal slicing would be easy to do within the ImageVerification project also). 

I went back to the requirements and started planning my tests, starting with basic validation, and then through red-green-refactor I got a nicely encapsulated ImageUpload domain class, that took in an inputDTO and produces an outputDTO. This is what is known as Classist TDD.

Next came the Service to support the ImageUpload objects, I went to a more Mockist approach with this, deciding that I needed the repository and storage interfaces, this allowed me to make simple mocks of them, this allowed me to define what the outputs into my mocked I/O clases would be, and therefore use those in my tests to help define the Service.

The service didn't take long to sort out, next came writing and wiring up the I/O, at this point manual testing is the way forward, and took the most time.

I made the front end with next.js, which is based on react, but has server side rendering as the default, with client side rendering available. This is the least fleshed out part, I ran into Cors issues and image loading issues (it only allows local images to be within the public folder within the backend folder?!), these issues lead me to add a very strange looking localDirectory to the application.Development.json, and some other hacky things that I didn't like too much.

The images are being uploaded to blob storage, and a log of each upload is being saved in an in memory database (it was the quickest way to sort that out without installing a local DB instance and doing migrations), the frontend doesn't like getting the images from blob storage though.

This is where I got to, I thought about using a signalR but to push messages to the user upon successful upload, but the latency seemed pretty low, and this would be an enhancement for later. I would also like to get the cloud based images showing in the frontend.

if you have any questions, give me a call.