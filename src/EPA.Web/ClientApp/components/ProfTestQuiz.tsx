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
    resinfo: ResultsInfo;
    resloading: boolean;
}

interface ResultsInfo {
    profSpecialties: UserSpeciality[];
    profDirection: string;
}

interface UserSpeciality {
    specialtyName: string;
    university: string;
    district: string;
    address: string;
    site: string;
}

export class ProfTestQuiz extends React.Component<RouteComponentProps<{}>, TestQuiz> {
    constructor() {
        super();
        this.state = {
            que: [], loading: true, submitted: false, selectedValue: [], totalScore: 0,
            resinfo: { profDirection: "", profSpecialties: [] }, resloading: true
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
                    loading: false
                });
            });
    }

    renderTestResults() {
        return <section className="main-settings">
            <div className="rec-center">
                <h1 className="text-center uni-recom"> Ваш результат - {this.state.resinfo.profDirection}</h1>
                <h2 className="uni-recom recom-padding rec-pad-left">Наші рекомендації:</h2>
                <section className='rec-pad-left'>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Спеціальність</th>
                                <th>Університет</th>
                                <th>Область</th>
                                <th>Адреса</th>
                                <th>Сайт</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.resinfo.profSpecialties.map(cont =>
                                <tr>
                                    <td>{cont.specialtyName}</td>
                                    <td>{cont.university}</td>
                                    <td>{cont.district}</td>
                                    <td>{cont.address}</td>
                                    <td>{cont.site}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </section>
            </div>
        </section>
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

    fetchResInfo() {
        fetch("api/profTest/"+this.props.match.params['id']+"/result", {
            method: 'POST',
            body: JSON.stringify(this.state.totalScore),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.json() as Promise<ResultsInfo>)
            .then(data => {
                this.setState({resinfo:data, resloading:false});
            });
    }

    public render() {
        let content = this.state.loading
            ? <p><em>Loading...</em></p>
            : !this.state.submitted
                ? this.renderTestQuiz()
                : this.state.resloading
                    ? <p>Loading...</p>
                    : this.renderTestResults();
        return <div className="quiz-pad">
            {content}
        </div>
    }

    submitScore() {
        let booly = true;
        if (booly){
            let score = 0;
            this.state.selectedValue.map(scr => score += scr);
            this.setState({
                submitted: true,
                totalScore: score
            });
            this.fetchResInfo();
        }
        else {
            alert('finish the test!1');
        }
    }
}


