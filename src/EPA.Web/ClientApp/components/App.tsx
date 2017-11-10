import * as React from 'react';
import { RouteComponentProps, Switch, Route } from 'react-router';
import { Layout } from './Layout';
import { Home } from './Home';
import { ProfTest } from './ProfTest';
import { TestInfo } from './TestInfo';
import { ProfTestQuiz } from './ProfTestQuiz';
import { Error404 } from './errors/Error';

interface Applicat{
    isError: boolean,
    errorMessage: string
}

export class App extends React.Component<{}, Applicat> {
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

    render() {
        let content = <Layout>
            {this.state.isError ? <Error404 {...this.props} message={this.state.errorMessage} />:
            <Switch>
                <Route exact path='/' render={(props) => (<Home {...props} message={this.state.errorMessage} />)} />
                <Route exact path='/quiz/:id' render={(props) => (<ProfTestQuiz {...props} />)} />
                <Route exact path='/testInfo/:id' render={(props) => (<TestInfo {...props} onError={this.onError}  />)} />
                <Route exact path='/profTest' render={(props) => (<ProfTest {...props} />)} />
                <Route path='*' render={(props) => (<Error404 {...props} message={this.state.errorMessage}/>)} />
                </Switch>}
        </Layout>

        return content;
    }

    
}