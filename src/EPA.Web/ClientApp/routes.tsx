import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { ProfTest } from './components/ProfTest';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/profTest' component={ProfTest} />
</Layout>;
