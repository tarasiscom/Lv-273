import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import  VirtualizedSelect  from 'react-virtualized-select'
import ListSpecialties from './ListSpecialties'
import { Loading } from './Loading';
import 'react-virtualized/styles.css'
import 'react-select/dist/react-select.css'
import 'isomorphic-fetch';
import ReactPaginate from 'react-paginate';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';

interface Specialities {
    districtId: number;
    subjectsIds: number[];
    subjects: Subject[];
    specialties: Specialty[];
    districts: District[];
    selectValueSubjects: { label: string, value: number }[];
    selectDistrict: { label: string, value: number };
    count: Count;
    loading: boolean;
    loadingDirectionsAndDistricts: boolean;
    isSubmitted: boolean;
}

interface Count {

    allElements: number;
    forOnePage: number;
}

interface DistrictDTO {
    name: string;
    id: number;
}

class District {
    value: number;
    label: string;
    constructor(value: number, label: string) {
        this.value = value;
        this.label = label;
    }
}

interface SubjectDTO {
    id: number;
    name: string
}

class Subject {
    value: number;
    label: string;

    constructor(value: number, label: string) {
        this.value = value;
        this.label = label;
    }
}

interface Specialty {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: SubjectDTO[];
}

export class ChooseSpecialtiesBySubject extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, Specialities> {

    constructor()
    {
        super();
        this.state = {

            subjectsIds: [],
            districtId: 0,
            subjects: [],
            specialties: [],
            districts: [],
            selectValueSubjects: [],
            selectDistrict: { value: 0, label: "Всі" },
            count: { allElements: 1, forOnePage: 1 },
            loading: true,
            loadingDirectionsAndDistricts: true,
            isSubmitted: false
        }
    }
            
    public render() {
        if (this.state.loadingDirectionsAndDistricts) {
            return <Loading />
        }
        else {
            return <div className="pad-for-footer">
                <div className="delete-margin">
                    <section className="jumbotron center-block">
                        <div className="container">
                            <div className="navigate">
                                <div className="virtselect col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2"><p>Предмети</p>
                                    <VirtualizedSelect multi={true}
                                        options={this.state.subjects}
                                        onChange={this.handleOnChangeSubjects}
                                        value={this.state.selectValueSubjects}>
                                    </VirtualizedSelect>
                                </div>
                                <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
                                    <VirtualizedSelect multi={false}
                                        options={this.state.districts}
                                        onChange={this.handleOnChangeDistrict}
                                        value={this.state.selectDistrict}>
                                    </VirtualizedSelect>
                                </div>
                                <div>
                                    <button className="col-md-offset-1  col-md-1 col-sm-offset-1 col-sm-2  col-xs-8 col-xs-offset-2 btn btn-primary cus-margin"
                                        onClick={this.handleOnClick}> Пошук
                                    </button>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
                {
                    this.state.isSubmitted ?
                        this.renderSubmitted() :
                        this.renderListSpecialies()
                }
                <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
            </div>
        }
    }

    private renderListSpecialies() {
        let tabbord;

        if (this.state.count.allElements == 0) {
            tabbord = <div>
                <h1>По даному запиту нічого не знайдено. Виберіть інші предмети, або область.</h1>
            </div>
        }
        else {
            tabbord = <ListSpecialties specialties={this.state.specialties} />
        }

        let pagin;
        if (this.state.count.allElements > 10) {
            pagin = <ReactPaginate
                previousLabel={"Попередня"}
                nextLabel={"Наступна"}
                breakLabel={<a>...</a>}
                pageCount={this.state.count.allElements / this.state.count.forOnePage}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={this.handlePageClick}
                containerClassName={"pagination"}
                subContainerClassName={"pages pagination"}
                selected={0}
                activeClassName={"active"} />
        }

        return <div className="container">
            <div className="col-md-10 col-md-offset-1">
                {tabbord}
                <div className="pageBar">
                    {pagin}
                </div>
            </div>
        </div>

    }

    private renderSubmitted() {
        return this.state.loading ?
            <Loading /> :
            this.renderListSpecialies();
    }

    componentDidMount() {
        this.fetchAllSubjects();
        this.fetchAllDistricts();
    }

    private fetchAllSubjects() {

        GetFetch<SubjectDTO[]>( 'api/ChooseSpecialties/subjectsList')
            .then(data => {
                this.setState({
                    subjects: data.map<Subject>(subject => new Subject(subject.id, subject.name)),
                    loadingDirectionsAndDistricts: false
                });
            })
            .catch(er => this.props.onError(er))
    }
    private fetchAllDistricts() {

        GetFetch<DistrictDTO[]>('api/ChooseSpecialties/districtsList')
            .then(data => {
                this.setState({
                    districts: data.map<District>(district => new District(district.id, district.name)),
                    loadingDirectionsAndDistricts: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    private submitFilter(selectValueSubmit, districtValueSubmit) {
        if (selectValueSubmit != null && selectValueSubmit.length > 0 && districtValueSubmit) {
            this.setState({
                isSubmitted: false,
                loading: true
            });
            let result: number[];
            result = [];
            for (let i = 0; i < selectValueSubmit.length; i++) {
                result.push(selectValueSubmit[i].value)
            }

            let subjectsAndDistrict = {
                listSubjects: result,
                district: districtValueSubmit.value
            }

            PostFetch<any>('api/ChooseSpecialties/count/bySubjects', { listSubjects: result, district: districtValueSubmit.value })
                .then(data => {
                    this.setState({
                        count: data,
                        isSubmitted: true
                    })
                })
                .catch(er => this.props.onError(er))

            this.fetchDataSpecialties(subjectsAndDistrict);
            this.setState({ districtId: districtValueSubmit.value, subjectsIds: result });
        }
        else {
            alert('Pick out some subjects or select district');
        }
    }

    private handlePageClick = (data) => {
        let selected = data.selected;
        let subjectsAndDistrict = {
            listSubjects: this.state.subjectsIds,
            district: this.state.districtId,
            countOfElementsOnPage: this.state.count.allElements,
            page: selected
        }

        this.fetchDataSpecialties(subjectsAndDistrict);
    }

    private fetchDataSpecialties(subjectsAndDistrict: object) {

        PostFetch<any>('api/ChooseSpecialties/bySubject', subjectsAndDistrict)
            .then(data => {
                this.setState({
                    specialties: data,
                    loading: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    private handleOnChangeSubjects = (valueArray) => {
        this.setState({ selectValueSubjects: valueArray })
    }

    private handleOnChangeDistrict = (selectDistricty) => {
        this.setState({ selectDistrict: selectDistricty })
    }

    private handleOnClick = () => {
        this.submitFilter(this.state.selectValueSubjects, this.state.selectDistrict)
    }
}
