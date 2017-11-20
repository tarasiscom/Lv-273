import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Radar from 'react-d3-radar';
import ListSpecialties from './ListSpecialties'
import ReactPaginate from 'react-paginate';
import { ErrorHandlerProp } from './App';
import { Loading } from './Loading';

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
    list: Specialty[];
    count: number;
}

interface Subject {
    id: number;
    name: string
}

interface GeneralTest {
    specialties: SpecialtyInfo;
    countsOfElementsOnPage: number
    idCurrentDirection: number;
    maxScore: number;
}

export default class TestResults extends React.Component<GeneralDirectionResult & ErrorHandlerProp, GeneralTest> {

    constructor(props) {
        super(props);
        this.state = { specialties: { list: [], count: 0 }, maxScore: this.GetDomainMax(), countsOfElementsOnPage: 15, idCurrentDirection: this.GetGeneralDirectionWithMaxScore().generalDir.id }
        this.GetSpecialties(this.state.idCurrentDirection, 1);
    }
    public render() {
        let loading = <Loading />
        let content = <div className="row">
            <div className="radar-position col-md-9 col-md-offset-3 col-sm-9 col-sm-offset-3 col-xs-9 col-xs-offset-3 col-lg-offset-1 col-lg-4 col-xl-6">
                {this.drawRadar()}
                <div className="row">
                    <h3>
                        Ваш результат - {this.GetGeneralDirectionWithMaxScore().generalDir.name}
                    </h3>
                </div>
            </div>
            <div className="pad-for-nav col-lg-offset-5 col-md-12  col-sm-12 col-xs-12 col-lg-7 col-xl-6" >

                <ListSpecialties specialties={this.state.specialties.list} />
                <div className="pageBar">
                    <ReactPaginate
                        previousLabel={"Попередня"}
                        nextLabel={"Наступна"}
                        breakLabel={<a>...</a>}
                        breakClassName={"break-me"}
                        pageCount={this.state.specialties.count / this.state.countsOfElementsOnPage}
                        marginPagesDisplayed={2}
                        pageRangeDisplayed={5}
                        onPageChange={this.handlePageClick}
                        containerClassName={"pagination"}
                        subContainerClassName={"pages pagination"}
                        activeClassName={"active"} />
                </div>
            </div>
        </div>
        return <div>{content}</div>
    }

    handlePageClick = (data) => {
        let selected = data.selected;
        this.GetSpecialties(this.state.idCurrentDirection, selected);
    }
    drawRadar() {
        return <div className="text-left">
            <Radar className="radar"
                width={450}
                height={450}
                padding={60}
                domainMax={this.state.maxScore}
                data={{
                    variables: this.props.testresult.map(gen =>
                        ({
                            key: gen.generalDir.name.toLowerCase(),
                            label: <a className="labelradar" onClick={this.GetSpecialties.bind(this, gen.generalDir.id, 1)}>{gen.generalDir.name}</a>
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

        this.setState({ idCurrentDirection: id });
        let directionInfo = {
            generaldirection: this.state.idCurrentDirection, page: selectedPage, countofelementsonpage: this.state.countsOfElementsOnPage
        }
        fetch('api/ChooseSpecialties/bydirectiononly', {
            method: 'POST',
            body: JSON.stringify(directionInfo),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.ok ? response.json() as Promise<SpecialtyInfo> : this.props.onError(response.status.toString()))
            .then(data => {
                this.setState({
                    specialties: data,
                })
            })
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