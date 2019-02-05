## Talent Competition Code Repository

This project will help interns at MVP Studio to understand how ReactJs, C# Web Api, MongoDb is used in Talent Code Architecture. 

Please follow below instruction to understand how to get started. If you have any questions, please check if it's asked on QuestionHub or raise the question their to get support. 

### React tutorials/resources
* https://reactjs.org/docs/hello-world.html
* MVP Studio React Training.pdf can be found here
ReactExamples.zip can be found [here](https://drive.google.com/file/d/1dXZeb3hmMsYbE1hmGEkb4_hyOkNiAbPa/view?usp=sharing)

React coding examples in ReactExamples.zip:
*ReactHelloWorld.html: Printing hello world using React
*ReactTimeline.css: CSS File for Timeline example
*ReactTimeline.html: React components and container example
*ReactTimelinePassObject.html: Passing a prop as an attribute and javascript object example
*ReactTimelinePassArray.html : Passing an array of javascript objects example

### Do’s and don’ts
See the [coding guidelines](http://git.mvp.studio/talent-competition/talent-competition/wikis/guides/coding-guidelines)  
Please try not to add styles inside the jsx file. Instead, please add your styles to TalentTheme.css.  
Please use ES6, jsx, use state  
Limit your use of javascript or jQuery - jQuery is required for AJAX, but very little else.  
Use AJAX to get/send data to the controller  
Please use plain javascript not typescript  

**Note : Make sure that you have Visual Studio 2017 installed in your computer.
Visual Studio 2015 does not work with ReactJS**

### Install react, babel, webpack, js tokens and react tags:
* Find the folder that contains webpack.config.js in the solution explorer
* Right click on the folder and select 'Open Folder in File Explorer'
* Open command prompt (windows + R, type cmd) and go to the folder that contains webpack.config.js (E.g: cd C:\Talent\Talent\Talent.WebApp\Scripts\react)
Install npm util packages:
`npm install`
* Check webpack version (make sure it's 4.5.0):
`webpack -version`

### Launch Talent project
[Check the wiki](http://git.mvp.studio/talent-competition/talent-competition/wikis/Starting-the-project) for more details.
* Get the latest source via Source Control Explorer
* Run webpack:
`cd C:\Talent\Talent\Talent.WebApp\Scripts\react`
`npm run build`
* Launch Talent.WebApp project in Visual Studio. Register an account using your email address and log in.


### Summary for add a new component: (detailed steps can be found below)
* Create cshtml file, .jsx files, .js file 
* Create a new entry in the webpack.config.js
* Run `npm run build` command in cmd 
(If webpack is already running, stop it and rerun the command. 
Or else, it would not read the new entry in the webpack.config.js and will not generate the bundle.)
* In the html page, add a link to the newly generated bundle

### Create cshtml page (e.g. under Mars.WebApp\Views\Home) and create a div with an id.
This Id will be used to select the element to render the main component.
```javascript
@{
	ViewBag.Title = "Search";
	Layout = "~/Views/Shared/_LayoutSearch.cshtml";
 }
<div id="service-search-section"></div>
```

### Create jsx components (under 'react/ReactScripts' folder)
* Create a new folder and add container (.jsx files) & components files (.jsx files) inside
  in the container file, import the required components.


### Create .js file (under to import jsx files and render components to html file)
* searchResult.bundle will be created under the /Scripts/react/dist folder when you save your code
* .js file Sample:
    ```javascript
    import React from "react";
    import ReactDOM from 'react-dom';
    import SearchResult from './Search/SearchResult.jsx';

    ReactDOM.render(
        <SearchResult />,
        document.getElementById('service-search-section')
    )
    ```

* Add a new entry in the webpack.config.js file
    ```javascript
         entry: {
    	   accountProfile: './ReactScripts/AccountProfile.js',
    	   homePage: './ReactScripts/Home.js',
    	   searchResult: './ReactScripts/SearchResult.js'
	},
    ```

* Run webpack:** `npm run build`
(If webpack is already running, stop it and rerun the command.)

* Add link to auto-generated bundle in the html page.*
    ```javascript
    @{
        ViewBag.Title = "Search";
	Layout = "~/Views/Shared/_LayoutSearch.cshtml";
    }
    <div id="service-search-section"></div>
    <script src="~/Scripts/react/dist/searchResult.bundle.js"></script>
    ```

### React tips
* Common coding mistakes using jsx
* Class names: class (html) => className (jsx), tabindex (html) => tabIndex (jsx)
* Require closing parent element or fragments: https://reactjs.org/docs/fragments.html
* Jsx Closing tags differ from html tags, you must have a closing tag for images and inputs: `<img></img>, <input</input>`
* Forgetting to turn on webpack : `npm run build`
* Forgetting to clear the cache
