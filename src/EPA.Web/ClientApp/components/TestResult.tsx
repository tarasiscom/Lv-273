import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Radar from 'react-d3-radar';
import ListSpecialties from './ListSpecialties'

interface TestResult {
    generalDir: GeneralDir;
    score: number;
}

interface GeneralDir {
    id: number;
    name: string;
    description: string;
}
interface GeneralDirectionResult {
    testresult: TestResult[];
}
interface Specialty {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];

}
interface Subject {
    id: number;
    name: string
}
interface GeneralTest {
    specialties: Specialty[];
    currentid: number;
    maxscore: number;
}
export default class TestResults extends React.Component<GeneralDirectionResult, GeneralTest> {

    constructor(props: GeneralDirectionResult) {
        super(props);
        this.state = { currentid: this.GetGeneralDirectionWithMaxScore().generalDir.id, specialties: [], maxscore: this.GetDomainMax() }
        this.GetSpecialties(this.state.currentid);
    }
    public render() {
        let loading = <p><em>Loading...</em></p>
        let content = <div className="row">
                            <div className="affix col-md-5 col-sm-5 col-xs-5">
                                {this.drawRadar()}
                            </div>
                            <div className="col-md-offset-5 col-md-7 col-sm-offset-5 col-sm-7 col-xs-offset-5 col-xs-7">
                                <ListSpecialties specialties={this.state.specialties} />
                            </div>
                    </div>
        return <div>{content}</div>
    }

    drawRadar() {
        return <div className="text-left">
            <Radar className="radar"
                width={450}
                height={450}
                padding={60}
                domainMax={this.state.maxscore}
                data={{
                    variables: this.props.testresult.map(gen =>
                        ({
                            key: gen.generalDir.name.toLowerCase(),
                            label: <a className="labelfont" onClick={this.GetSpecialties.bind(this, gen.generalDir.id)}> {gen.generalDir.name}</a>
                        }),
                    ),
                    sets:
                    [{
                        key: 'user',
                        label: 'User Results',
                        values: this.props.testresult.reduce((o, testres) =>
                            ({ ...o, [testres.generalDir.name.toLowerCase()]: testres.score }), {}),
                    },
                    ],
                }}

            />
        </div>
    }

    GetSpecialties = (id) => {
        
        this.setState({ currentid: id })
        
        fetch('api/choosespeciality/bydirectiononly', {
            method: 'POST',
            body: JSON.stringify(this.state.currentid),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<Specialty[]>)
            .then(data => {
                this.setState({
                    specialties: data,
                }) })
    }
    private GetDomainMax() {
        var max = this.GetGeneralDirectionWithMaxScore().score;
        if (max % 5 > 2)
            return max + (5 - max % 5);
        max = (max & 1) == 0 ? max : max + 1;
        return max;
    }
    private GetGeneralDirectionWithMaxScore() {
        var arrScores = this.props.testresult;
        var max = arrScores[0];

        for (let i = 1; i < arrScores.length; i++) {
            if (arrScores[i].score > max.score) {
                max = arrScores[i];
            }
        }
        return max;
    }
}