import * as React from 'react';
import { RouteComponentProps, Switch, Route, BrowserRouter } from 'react-router-dom';
import { Layout } from './Layout';
import { Home } from './Home';
import { ProfTest } from './ProfTest';
import { TestInfo } from './TestInfo';
import { ChooseSpecialtiesBySubject } from './ChooseSpecialtiesBySubject'
import { ChooseSpecialty } from './ChooseSpecialty';
import { ChooseSpecialtiesByDirection } from './ChooseSpecialtiesByDirection';
import { TestQuiz } from './TestQuiz';
import { ErrorPage } from './errors/Error';
import PropTypes from 'prop-types';

export interface ErrorHandlerProp {
    onError: PropTypes.func
}

interface AppErrorHandler{
    isError: boolean,
    errorMessage: string
}

export class App extends React.Component<{}, AppErrorHandler> {
    constructor() {
        super();
        this.state = {
            isError: false,
            errorMessage: '404'
        };
    }

    onError = (message) => {
        this.setState({ isError: true, errorMessage: message });
    }

    cleanError() {
        this.setState({ isError: false });
    }

    render() {
        const errRoute = (<Route path='*' render={(props) => (<ErrorPage {...props} message={this.state.errorMessage} onRouteChange={this.cleanError.bind(this)} />)} />);
        return (
            <Layout>
                {
                    this.state.isError ?
                        errRoute
                        :
                        <Switch>
                            <Route exact path='/' render={(props) => (<Home {...props} />)} />
                            <Route exact path='/profTest' render={(props) => (<ProfTest {...props} onError={this.onError} />)} />
                            <Route exact path='/testInfo/:id' render={(props) => (<TestInfo {...props} onError={this.onError} />)} />
                            <Route exact path='/quiz/:id' render={(props) => (<TestQuiz {...props} onError={this.onError} />)} />
                            <Route exact path='/ChooseSpecialty' component={ChooseSpecialty} />
                            <Route exact path='/ChooseSpecialties/bySubject' component={ChooseSpecialtiesBySubject} />
                            <Route exact path='/ChooseSpecialties/byDirection' component={ChooseSpecialtiesByDirection} />
                            {errRoute}
                        </Switch>
                }
            </Layout>
        )        
    }

    
}