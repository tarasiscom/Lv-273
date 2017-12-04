﻿;import * as React from 'react';
import { RadioGroup, Radio } from 'react-radio-group';

interface PropTypes {
    questionNumber: number;
    question: TestQuestion;
    onAnswerChoose: Function;
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

export class Question extends React.Component<PropTypes, {}> {
    constructor(props: PropTypes) {
        super(props);
        this.state = {
            loading: true
        }
    }

    handleClick(id:number) : void {
        this.props.onAnswerChoose(id+1);
    }

    render() {

        const listAnswers = this.props.question.answers.map((item, id) => {
            return (
                <label key={this.props.questionNumber + '.' + id} className="btn btn-lg btn-primary btn-block element-animation"
                    onClick={() => this.handleClick(id)}>
                    <span className="btn-label">
                        <i className="glyphicon glyphicon-chevron-right"></i>
                    </span>
                    {item.text}
                </label>
            );
        });


        return (<div className="text-center">
            <div className="question-container">
                <section className="jumbotron text-center">
                    <div className="container">
                        <h3 className="jumbotron-heading">{this.props.question.text}</h3>
                    </div>
                </section>
            </div>
            <div className="h-100 row align-items-center">
                <div className="col-md-6 col-md-offset-3">
                    {listAnswers}
                </div>
            </div>
        </div>);
    }
};