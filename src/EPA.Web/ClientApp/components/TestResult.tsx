import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Radar from 'react-d3-radar';
import ListSpecialties from './ListSpecialties'
import ReactPaginate from 'react-paginate';
import { ErrorHandlerProp , GetFetch } from './App';
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
    id: number;
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
    isFavorite: boolean;

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
    loadingSpecialties: boolean;
}

export default class TestResults extends React.Component<GeneralDirectionResult & ErrorHandlerProp, GeneralTest> {

    constructor(props) {
        super(props);
        this.state = {
            specialties: [],
            maxScore: this.getDomainMax(),
            idCurrentDirection: this.getGeneralDirectionWithMaxScore().generalDir.id,
            count: { allElements: 1, forOnePage: 1 },
            loadingSpecialties: true
        }
        this.fetchAllSpecialties(this.state.idCurrentDirection);
    }
    
    getDirectionName(id: number) {
        let name = this.props.testresult;
        return name;
    }

    public render() {
        let content = <div className="row">
            <div className="col-xs-12 col-sm-12 col-md-6 col-lg-6 center-block">
                {this.drawRadar()}
                <div className="row">
                    <h3 className="text-center">
                        Ваш результат - {this.getGeneralDirectionWithMaxScore().generalDir.name}
                    </h3>
                </div>
            </div>
            <div className="pad-for-nav-testres col-xs-12 col-sm-12 col-md-6 col-lg-6" >
            { this.state.loadingSpecialties ? <div className= "text-center"><Loading /></div>
                    : <div>
                 <h4 className="direction-label-pad">Галузь - {this.props.testresult[this.state.idCurrentDirection-1].generalDir.name}</h4>
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
                                page={0}
                                selected={0}
                                subContainerClassName={"pages pagination"}
                                activeClassName={"active"} />
                       </div>
                </div>
            }
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
                                        onClick={this.fetchAllSpecialties.bind(this, gen.generalDir.id)}>
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
        GetFetch<any>('api/ChooseSpecialties/byDirectionAndDistrict/' + id + '/' + 0 + '/' + selectedPage)
            .then(data => {
                this.setState({
                    specialties: data, loadingSpecialties: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    private fetchAllSpecialties = (id) => {
        this.setState({ loadingSpecialties: true });
        GetFetch<any>('api/ChooseSpecialties/count/' + id + '/' + 0 + '/')
            .then(data => {
                this.setState({ count: data })
            })
            .catch(er => this.props.onError(er))

        this.setState({ idCurrentDirection: id });
        this.getSpecialties(id, 0);
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