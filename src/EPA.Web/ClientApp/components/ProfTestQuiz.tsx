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
    name: string;
    ans: TestAnswer[]
}

interface TestAnswer {
    name: string;
    pts: number;
}


interface TestQuiz {
    id: number;
    que: TestQuestion[];
    loading: boolean;
    selectedValue: number;
    totalScore: number;
}

export class ProfTestQuiz extends React.Component<RouteComponentProps<{}>, TestQuiz> {
    constructor() {
        super();

        this.state = {
            id: 0, que: [], loading: true, selectedValue: 0, totalScore: 0
        };


        //это не будет работать после 10
        let pathId = window.location.pathname.substr(window.location.pathname.length - 1, window.location.pathname.length);
        let path = 'api/profTest/' + pathId + '/questions';

        fetch(path)
            .then(response => response.json() as Promise<TestQuiz>)
            .then(data => {
                this.setState({ id: data.id, que: data.que, loading: false });
            });
    }

    handleChange(value) {
        this.setState({ selectedValue: value });
    }


    public render() {
        return <div className="container">
            <div className="row">
                {this.state.que.map(que =>

                    <div>
                        <p>Question #{que.id}: {que.name}</p>
                        <RadioGroup name={que.id.toString()} selectedValue={this.state.selectedValue} onChange={this.handleChange}>
                            {que.ans.map(ans => 
                                <div className="row" >
                                    <label>
                                        <Radio value={ans.pts} />
                                        {ans.name}
                                    </label>
                                </div>
                                )}
                        </RadioGroup>
                    </div>
                )}
            </div>
            <button onClick={() => this.submitScore()}>
                Submit
            </button>

            {console.log(this.state.selectedValue )}

            {/*

                нужно сделать submitScore (
                понять как добраться до значения нажатого радиобокса(видимо как-то через селектед вэлью )
                и походу ка краз с ним и все проблемы
                )

                и переназначить получаемые данные 

            */}


        </div>
    }


    submitScore() {
        

        this.setState({
            totalScore: 1
        });
    }

}


