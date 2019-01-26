import React  from 'react';
import {Route, IndexRoute} from 'react-router';
import App from './Components/App';
import HomePage from './Components/home/HomePage';
import AboutPage from './Components/about/AboutPage';
import FilesPage from './Components/downloadedFiles/FilesPage';
import ManageFilePage from './Components/downloadedFiles/ManageFilePage';

export default(
    <Route path="/" component={App}>
    <IndexRoute component={HomePage}/>
    <Route path="about" component={AboutPage}/>
    <Route path="file" component={ManageFilePage}/>
    <Route path="file/:Id" component={ManageFilePage}/>
    <Route path="downloadedFiles" component={FilesPage}/>
    </Route>
);