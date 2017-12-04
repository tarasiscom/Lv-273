import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import Paginate from 'react-pagination-component'
import { Question } from './Question';
import TestResults from './TestResult';
import { ErrorHandlerProp , GetFetch, PostFetch} from './App';
import { Loading } from './Loading';


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



export class TestQuiz extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {
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
    }

    onAnswerChoose(answId: number): void {
        let updatedAnswers = this.state.userAnswers.slice();
        updatedAnswers.push({
            idQuestion: this.state.questions[this.state.currentPage - 1].id,
            idAnswer: answId
        });

        let nextPage = this.state.currentPage + 1;
        this.setState({
            userAnswers: updatedAnswers,
            currentPage: nextPage
        });
    }

    loadQuestions() {
        let pathId = this.props.match.params['id'];
        let path = 'api/profTest/' + pathId + '/questions';

        GetFetch<any>(path)
            .then(data => {
                this.setState({
                    questions: data,
                    loading: false
                });
            })
            .catch(er => this.props.onError(er))
        
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
        let pathId = this.props.match.params['id'];
        PostFetch<any>("api/profTest/" + pathId +"/result", this.state.userAnswers)
            .then(data => {
                this.setState({
                    testResult: data,
                    isSubmitted: true
                });
            })
            .catch(er => this.props.onError(er))
    }

    render() {
        if (this.state.loading) {
            return <Loading />
        }
        else {
            return <div className="pad-for-footer">{this.state.currentPage <= this.state.questions.length ? this.rendeQuiz() : this.renderResult()}</div>
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
            return <div><TestResults testresult={this.state.testResult} onError={this.props.onError} /></div>
        }
        else {
            this.submitTest();
            return <Loading />
        }
    };
};