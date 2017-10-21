import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
 
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';





interface TestDetailInformation {
    id: number;
    name: string;
    description: string;
    approximatedtime: number;
    questioncount: number;
    loading: boolean;
}

export class TestInfo extends React.Component<RouteComponentProps<{}>, TestDetailInformation> {
    constructor() {
        super();

        this.state = {
            id: 0, name: "", description: "", approximatedtime: 0, questioncount: 0, loading: true
        };

        
            fetch('api/profTest/1/info')
            .then(response => response.json() as Promise<TestDetailInformation>)
            .then(data => {
                this.setState({ id: data.id, name: data.name, description: data.description, approximatedtime: data.approximatedtime, questioncount: data.questioncount, loading: false });
            }); 
    }
    
    public render() {
        return <div>
            <h1 className="text-center">Профорієнтаційний тест</h1>
            
            <p className="text-center">Тут Ви можете переглянути інформацію про тест</p>
            <h1 className="text-center">{this.state.id}</h1>
            <h1 className="text-center">{this.state.name} </h1>
            <h1 className="text-center">{this.state.approximatedtime} </h1>
            <h1 className="text-center">{this.state.questioncount} </h1>
            </div>
    }


   
    
}
