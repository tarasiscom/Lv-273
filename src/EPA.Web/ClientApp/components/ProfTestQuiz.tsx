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
    selectedValue: string;
    totalScore: number;
}

export class ProfTestQuiz extends React.Component<RouteComponentProps<{}>, TestQuiz> {
    constructor() {
        super();

        this.state = {
            que: [], loading: true, selectedValue: '1', totalScore: 0
        };




    }

    componentWillReceiveProps(nextProps) {
        
        console.log("lalalallala");
    }

    componentDidMount() {
        this.fetchData()
    }


    handleChange(value) {

        console.log("New value: ", value);

        this.setState({ selectedValue: value });
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

    public render() {
        
        
        return <div className="container">
            <div className="row" id="testsubmit">
                {this.state.que.map(que =>

                    <div>
                        <p>Question #{que.id}: {que.question}</p>
                        <RadioGroup name={que.id.toString()} selectedValue={this.state.selectedValue} onChange={this.handleChange}>
                            {que.answer.map(ans => 
                                <div className="row" >
                                    <label>
                                        <Radio value={ans.point.toString()} />
                                        {ans.answer}
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

        let parent = document.getElementById('testsubmit');
        

        this.setState({
            totalScore: 1
        });

    }

}


