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

interface Count {
    allElements: number;
    forOnePage: number;
}

interface Subject {
    id: number;
    name: string
}

interface GeneralTest {
    specialties: Specialty[];
    idCurrentDirection: number;
    maxScore: number;
    count: Count;
}

export default class TestResults extends React.Component<GeneralDirectionResult & ErrorHandlerProp, GeneralTest> {

    constructor(props) {
        super(props);
        this.state = {
            specialties: [],
            maxScore: this.getDomainMax(),
            idCurrentDirection: this.getGeneralDirectionWithMaxScore().generalDir.id,
            count: { allElements: 1, forOnePage: 1 }
        }
        this.fetchAllSpecialties(this.state.idCurrentDirection, 0);
    }
    public render() {
        let loading = <Loading />
        let content = <div className="row">
            <div className="col-xs-12 col-sm-12 col-md-6 col-lg-6 center-block">
                {this.drawRadar()}
                <div className="row">
                    <h3 className="text-center">
                        Ваш результат - {this.GetGeneralDirectionWithMaxScore().generalDir.name}
                    </h3>
                </div>
            </div>
            <div className="pad-for-nav col-xs-12 col-sm-12 col-md-6 col-lg-6" >

                <ListSpecialties specialties={this.state.specialties} />
                    <div className="pageBar">
                        <ReactPaginate
                                previousLabel={"Попередня"}
                                nextLabel={"Наступна"}
                                breakLabel={<a>...</a>}
                        breakClassName={"break-me"}
                                pageCount={this.state.count.allElements / this.state.count.forOnePage}
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
        this.getSpecialties(this.state.idCurrentDirection, selected);
    }
    drawRadar() {
        return <div className="text-center" >
            <Radar className="radar"
                width={450}
                height={450}
                padding={60}
                
                domainMax={this.state.maxScore}
                data={{
                    variables: this.props.testresult.map(gen =>
                        ({
                            key: gen.generalDir.name.toLowerCase(),
                            label: <a className="labelradar"
                                onClick={this.fetchAllSpecialties.bind(this, gen.generalDir.id, 0)}>
                                {gen.generalDir.name}</a>
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

    private getSpecialties = (id, selectedPage) => {
        fetch('api/ChooseSpecialties/byDirectionAndDistrict/' + this.state.idCurrentDirection + '/' + 0 + '/' + selectedPage)
            .then(response => response.ok ? response.json() as Promise<Specialty[]> : this.props.onError(response.status.toString()))
            .then(data => {
                this.setState({
                    specialties: data,
                })
            })
    }

     private fetchAllSpecialties=(id, selectedPage) => {
        fetch('api/ChooseSpecialties/count/' + this.state.idCurrentDirection + '/' + 0 + '/')
            .then(response => response.json() as Promise<Count>)
            .then(data => {
                this.setState({ count: data })
            })
        console.log("count all is " + this.state.count.allElements);
        this.getSpecialties(id, selectedPage);
        this.setState({ idCurrentDirection: id });
    }

    private getDomainMax() {
        var max = this.getGeneralDirectionWithMaxScore().score;
        max = (max & 1) == 0 ? max : max + 1;
        return max;
    }
    private getGeneralDirectionWithMaxScore() {
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