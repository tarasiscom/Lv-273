import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import Paginate from 'react-pagination-component'
import { Question } from './Question';

interface stateTypes {
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
    generalDirection: GeneralDir;
    score: number;
}

interface GeneralDir {
    id: number;
    name: string;
    description: string;
}

export class TestQuiz extends React.Component<RouteComponentProps<{}>, stateTypes> {
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
        this.setState({
            userAnswers: updatedAnswers
        });

        let nextPage = this.state.currentPage < 30
            ? this.state.currentPage + 1
            : this.state.currentPage;
        this.changePage(nextPage);
    }

    loadQuestions() {
        let pathId = this.props.match.params['id'];
        let path = 'api/profTest/' + pathId + '/questions';
        fetch(path)
            .then(response => response.json() as Promise<TestQuestion[]>)
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
        }).then(response => response.json() as Promise<TestResult[]>)
            .then(data => {
                this.setState({
                    testResult: data,
                    isSubmitted: true
                });
            });
    }

    render() {
        let content = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.state.isSubmitted == false
                ? this.rendeQuiz()
                : this.renderResult();
        return <div>
            {content}
        </div>
        
    }

    rendeQuiz() {
        let submit = this.state.currentPage == 30
            ? this.renderSubmitButton()
            : <div></div>
        return <div className="col">
                    <Question questionNumber={this.state.currentPage}
                              question={this.state.questions[this.state.currentPage - 1]}
                              onAnswerChoose={this.onAnswerChoose} />
                    <div className="row submit_btn">{submit}</div>
                    <div className="pagin"><Paginate totalPage={this.state.questions.length} focusPage={this.changePage} /></div>
        </div>
    }

    renderSubmitButton()
    {
        return <div className="col-md-2 col-md-offset-5">
            <button className="btn btn-lg btn-block btn-success p-1" onClick={this.submitTest}>Завершити тест</button>
        </div>
    }

    renderResult() {
        console.log(this.state.testResult);
        return <div>Test tesult component</div>
    };
};