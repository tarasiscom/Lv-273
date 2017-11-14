import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import Paginate from 'react-pagination-component'
import { Question } from './Question';
import TestResults from './TestResult';
import PropTypes from 'prop-types';

interface StateTypes {
    questions: TestQuestion[];
    currentPage: number;
    loading: boolean;
    isSubmitted: boolean;
    userAnswers: UserAnswer[];
    testResult: TestResult[];
}

interface TestQuestion {
    id: number;
    text: string;
    answers: TestAnswer[],
}

interface TestAnswer {
    id: number;
    text: string;  
}

interface UserAnswer {
    idQuestion: number;
    idAnswer: number;
}

interface TestResult {
    generalDir: GeneralDir;
    score: number;
}

interface GeneralDir {
    id: number;
    name: string;
    description: string;
}


interface myProps {
    onError: PropTypes.func
}

export class TestQuiz extends React.Component<RouteComponentProps<{}>&myProps, StateTypes> {
    constructor() {
        super();
        this.state = {
            questions: [],
            currentPage: 1,
            loading: true,
            isSubmitted: false,
            userAnswers: [],
            testResult: [],
        }
        this.onAnswerChoose = this.onAnswerChoose.bind(this);
        this.submitTest = this.submitTest.bind(this);
    }

    onAnswerChoose(answId: number): void 
    {
        let updatedAnswers = this.state.userAnswers.slice();
        updatedAnswers.push({ idQuestion: this.state.questions[this.state.currentPage - 1].id, idAnswer: answId });        

        let nextPage = this.state.currentPage + 1;
        this.setState({
            userAnswers: updatedAnswers,
            currentPage: nextPage
        });
    }

    loadQuestions() {
        let pathId = this.props.match.params['id'];
        let path = 'api/profTest/' + pathId + '/questions';
        fetch(path)
            .then(response => response.ok ? response.json() as Promise<TestQuestion[]> : this.props.onError(response.status.toString()) )
            .then(data => {
                this.setState({
                    questions: data,
                    loading: false
                });
            });
    }

    componentWillMount() {
        this.loadQuestions();
    }

    changePage = (page) => {
        this.setState({
            currentPage: page
        });
    };  

    submitTest() {
        fetch("api/profTest/" + this.props.match.params['id'] + "/result", {
            method: 'POST',
            body: JSON.stringify(this.state.userAnswers),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.ok ? response.json() as Promise<TestResult[]> : this.props.onError(response.status.toString()) )
            .then(data => {
                this.setState({
                    testResult: data,
                    isSubmitted: true
                });
            });
    }

    render() {
        if (this.state.loading) {
            return <p><em>Loading...</em></p>
        }
        else {
            return <div>{this.state.currentPage <= this.state.questions.length ? this.rendeQuiz() : this.renderResult()}</div>
        }        
    }

    rendeQuiz() {
        return <div className="col margin-bottom">
                    <Question questionNumber={this.state.currentPage}
                              question={this.state.questions[this.state.currentPage - 1]}
                              onAnswerChoose={this.onAnswerChoose} />
                    <div className="pagin"><Paginate totalPage={this.state.questions.length} focusPage={this.changePage} /></div>
        </div>
    }
    
    renderResult() {
        if (this.state.isSubmitted) {
            return <div><TestResults testresult={this.state.testResult} /></div>
        }
        else {
            this.submitTest();
            return <p><em>Loading...</em></p>
        }
    };
};