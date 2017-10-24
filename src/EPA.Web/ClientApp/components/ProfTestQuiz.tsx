﻿import * as React from 'react';
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
            que: [], loading: true, submitted: false, selectedValue: [], totalScore: 0
        };




    }

    componentDidMount() {
        this.fetchData()
    }


    handleChange(value, index) {

        let selval = this.state.selectedValue;
        selval[index] = value;

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
                this.setState({
                    que: data,
                    //selectedValue: Array.apply(null, Array(data.length)).map(function (cur, index) { return data }),
                    loading: false
                });
            });


    }

    renderTestResults() {
        return <div>

            <p>{this.state.totalScore}</p>

        </div>

    }

    renderTestQuiz() {
        return <div className="row" id="testsubmit">
            {this.state.que.map((que, index) =>

                <div>
                    <p>Question #{que.id}: {que.question}</p>
                    <RadioGroup name={que.id.toString()} selectedValue={this.state.selectedValue[index]} onChange={(e) => this.handleChange(e, index)}>
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
            ? <p><em>Loading...</em></p>
            : !this.state.submitted
                ? this.renderTestQuiz()
                : this.renderTestResults()


        return <div className="container">

            {content}

        </div>
    }


    submitScore() {

        if (this.state.selectedValue.length==this.state.que.length){
            let score = 0;
            this.state.selectedValue.map(scr => score += scr);

            this.setState({
                submitted: true,
                totalScore: score
            });
        }
        else {
            alert('finish the god damn test!1');
        }
    }

}


