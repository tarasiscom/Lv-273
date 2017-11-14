import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Radar from 'react-d3-radar';
import ListSpecialties from './ListSpecialties'
import ReactPaginate from 'react-paginate';

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

interface SpecialtyInfo {
    listSpecialties: Specialty[];
    countOfAllElements: number;
}
interface Subject {
    id: number;
    name: string
}
interface GeneralTest {
    specialties: SpecialtyInfo;
    countsOfElementsOnPage: number
    idCurrentDirection: number;
    maxscore: number;
}
export default class TestResults extends React.Component<GeneralDirectionResult, GeneralTest> {

    constructor(props: GeneralDirectionResult) {
        super(props);
        this.state = { specialties: { listSpecialties: [], countOfAllElements:0 }, maxscore: this.GetDomainMax(), countsOfElementsOnPage: 15, idCurrentDirection: this.GetGeneralDirectionWithMaxScore().generalDir.id }
        this.GetSpecialties(this.state.idCurrentDirection, 1);
    }
    public render() {
        let loading = <p><em>Loading...</em></p>
        let content = <div className="row">
                            <div className="affix col-md-5 col-sm-5 col-xs-5">
                                {this.drawRadar()}
                            </div>
                            <div className="col-md-offset-5 col-md-7 col-sm-offset-5 col-sm-7 col-xs-offset-5 col-xs-7">

                <ListSpecialties specialties={this.state.specialties.listSpecialties} />
                                <ReactPaginate
                                previousLabel={"Попередня"}
                                nextLabel={"Наступна"}
                                breakLabel={<a>...</a>}
                                breakClassName={"break-me"}
                                pageCount={this.state.specialties.countOfAllElements / this.state.countsOfElementsOnPage} //ACHTUNG! HARDCODE!
                                marginPagesDisplayed={2}
                                pageRangeDisplayed={5}
                                onPageChange={this.handlePageClick}
                                containerClassName={"pagination"}
                                subContainerClassName={"pages pagination"}
                                activeClassName={"active"} />
                                <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
                            </div>
                    </div>
        return <div>{content}</div>
    }

    handlePageClick = (data) => {
        let selected = data.selected+1;
        this.GetSpecialties(this.state.idCurrentDirection, selected);
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
                            label: <a className="labelfont" onClick={this.GetSpecialties.bind(this, gen.generalDir.id, 1)}> {gen.generalDir.name}</a>
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

    GetSpecialties = (id, selectedPage) => {

        this.setState({idCurrentDirection: id});
        let directionInfo = {
            generaldirection: this.state.idCurrentDirection, page: selectedPage, countofelementsonpage: this.state.countsOfElementsOnPage
        }
        fetch('api/choosespeciality/bydirectiononly', {
            method: 'POST',
            body: JSON.stringify(directionInfo),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<SpecialtyInfo>)
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