import './css/content.css';
import './css/navbarAndFoter.css';
import './css/question.css';
import './css/university.css';
import 'bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { BrowserRouter, Route, Switch, Router } from 'react-router-dom';
import { App } from './components/App';

function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing
    // configuration and injects the app into a DOM element.
    //const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

    
    ReactDOM.render(
        <BrowserRouter>
            <App />
        </BrowserRouter>,
        document.getElementById('react-app')
    );

}

renderApp();

/*
// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        routes = require<typeof RoutesModule>('./routes').routes;
        renderApp();
    });
}
*/