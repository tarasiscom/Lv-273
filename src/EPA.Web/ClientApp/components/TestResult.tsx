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
}
export default class TestResults extends React.Component<GeneralDirectionResult, GeneralTest> {

    constructor(props: GeneralDirectionResult) {
        super(props);
        this.state = { specialties: [], currentid: this.GetGeneralDirectionWithMaxScore().generalDir.id}
    }
    public render() {
        let loading = <p><em>Loading...</em></p>
        let content = <div className="row">
                            <div className="affix col-md-5 col-sm-5 col-xs-5">
                                {this.drawRadar()}
                            </div>
                            <div className="col-md-offset-5 col-md-7 col-sm-offset-5 col-sm-7 col-xs-offset-5 col-xs-7">
                                {this.state.currentid != -1 ? this.RetrieveSpecialties(this.state.currentid) : loading}
                            </div>
                    </div>
        return <div>{content}</div>
    }

    drawRadar() {
        //this.props.match.params = 2;
        return <div className="text-left">
            <Radar className="radar"
                width={600}
                height={600}
                padding={70}
                domainMax={this.GetDomainMax()}
                //onHover={this.handleHover.bind(this)}
                data={{
                    variables: this.props.testresult.map(gen =>
                        ({
                            key: gen.generalDir.name.toLowerCase(),
                            label: <a className="labelfont" onClick={this.handleClick.bind(this, gen.generalDir.id)}> {gen.generalDir.name}</a>
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

    RetrieveSpecialties(id) {
        let directionAndDistrict = { GeneralDirection: id, District: 0 }
        fetch('api/choosespeciality/bydirection', {
            method: 'POST',
            body: JSON.stringify(directionAndDistrict),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<Specialty[]>)
            .then(data => { this.setState({ specialties: data }) })
        return <div> <ListSpecialties univers={this.state.specialties} /> </div>
    }
    handleClick = (id) => {
        
        console.log(id);
        this.setState({ currentid: id })
        //this.props.match.params = id;
    }
    private GetDomainMax() {
        var max = this.GetGeneralDirectionWithMaxScore().score;
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