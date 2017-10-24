import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import { RadioGroup, Radio } from 'react-radio-group';

import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';

interface TestQuestion {
    id: number;
    question: string;
    answer: TestAnswer[]
}

interface TestAnswer {
    answer: string;
    point: number;
}


interface TestQuiz {
    que: TestQuestion[];
    loading: boolean;
    submitted: boolean;
    selectedValue: number[];
    totalScore: number;
}

export class ProfTestQuiz extends React.Component<RouteComponentProps<{}>, TestQuiz> {
    constructor() {
        super();

        this.state = {
            que: [], loading: true, submitted: false, selectedValue: [
                1, 1
            ], totalScore: 0
        };




    }

    componentDidMount() {
        this.fetchData()
    }


    handleChange(value, curid) {
        
        let selval = this.state.selectedValue;
        selval[curid] = value;
        
        this.setState({
            selectedValue: selval
        }); 

    }

    fetchData() {

        let pathId = this.props.match.params['id'];
        let path = 'api/profTest/' + pathId + '/questions';
        fetch(path)
            .then(response => response.json() as Promise<TestQuestion[]>)
            .then(data => {
                this.setState({ que: data, loading: false });
            });

    }

    renderTestResults()
    {
        return <div>

            <p>{this.state.totalScore}</p>

            </div>

    }

    renderTestQuiz() {
        return <div className="row" id="testsubmit">
            {this.state.que.map(que =>

                <div>
                    <p>Question #{que.id}: {que.question}</p>
                    <RadioGroup name={que.id.toString()} selectedValue={this.state.selectedValue[que.id - 1]} onChange={(e) => this.handleChange(e, que.id - 1)}>
                        {que.answer.map(ans =>
                            <div className="row" >
                                <label>
                                    <Radio value={ans.point} />
                                    {ans.answer}
                                </label>
                            </div>
                        )}
                    </RadioGroup>
                </div>
            )}
            <button onClick={() => this.submitScore()}>
                Submit
            </button>
            </div>

    }


    public render() {

        let content = this.state.loading 
            ?<p><em>Loading...</em></p>
            : !this.state.submitted
                ? this.renderTestQuiz()
                : this.renderTestResults()
            
        
        return <div className="container">

            { content }
            
        </div>
    }


    submitScore() {
        
        let score = 0;
        this.state.selectedValue.map(scr => score += scr);
        console.log("score: " + score);
        this.setState({
            submitted: true,
            totalScore: score
        });

    }

}


