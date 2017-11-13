import * as React from 'react';
import { RouteComponentProps, Switch, Route, BrowserRouter } from 'react-router-dom';
import { Layout } from './Layout';
import { Home } from './Home';
import { ProfTest } from './ProfTest';
import { TestInfo } from './TestInfo';
import { ProfTestQuiz } from './ProfTestQuiz';
import { ErrorPage } from './errors/Error';

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
                            <Route exact path='/' render={(props) => (<Home {...props} message={this.state.errorMessage} />)} />
                            <Route exact path='/quiz/:id' render={(props) => (<ProfTestQuiz {...props} onError={this.onError} />)} />
                            <Route exact path='/testInfo/:id' render={(props) => (<TestInfo {...props} onError={this.onError} />)} />
                            <Route exact path='/profTest' render={(props) => (<ProfTest {...props} />)} />
                            {errRoute}
                        </Switch>
                }
            </Layout>           
        )
        
    }

    
}