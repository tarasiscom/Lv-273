/*import * as React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ProfTest } from './components/ProfTest';
import { TestInfo } from './components/TestInfo';
import { ProfTestQuiz } from './components/ProfTestQuiz';
import { Error404 } from './components/errors/404';

export const routes = <Switch>
    <Layout><Switch>
        <Route exact path='/' component={Home} />
        <Route exact path='/profTest' component={ProfTest} />
        <Route exact path='/testInfo/:id' component={TestInfo} />
        <Route exact path='/quiz/:id' component={ProfTestQuiz} /></Switch>
    </Layout>
    <Route path='*' component={Error404} />
</Switch>;

*/