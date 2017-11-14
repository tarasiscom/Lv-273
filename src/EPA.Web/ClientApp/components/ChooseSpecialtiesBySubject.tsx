import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import  VirtualizedSelect  from 'react-virtualized-select'
import ListSpecialties from './ListSpecialties'
import 'react-virtualized/styles.css'
import 'react-select/dist/react-select.css'
import 'isomorphic-fetch';
import  ReactPaginate  from 'react-paginate';

interface Specialities {
    subjects: Subject[];
    univers: SpecialtyInfo;
    districts: District[];
    selectValueSubjects: { label: string, value: number }[];
    selectDistrict: { label: string, value: number };
    page: number;
}


interface SpecialtyInfo {
    listSpecialties: Univer[];
    countOfAllElements: number;
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

interface Univer {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: SubjectDTO[];
}

export class ChooseSpecialtiesBySubject extends React.Component<RouteComponentProps<{}>, Specialities> {

    constructor()
    {
        super();
        this.state = {
            subjects: [],
            page: 0,
            univers: { listSpecialties: [], countOfAllElements:1 },
            districts: [],
            selectValueSubjects: [],
            selectDistrict: { value: 0, label: "Всі" }
        }
    }

    componentDidMount() {
        this.fetchDataSubject();
        this.fetchAllDistricts();
    }

    fetchDataSubject() {
        fetch('api/ChooseUniversity/ChoseSpecBySub')
            .then(response => response.json() as Promise<SubjectDTO[]>)
            .then(data => {
                this.setState({
                    subjects: data.map<Subject>(subject => new Subject(subject.id, subject.name))
                });
            });
    }
    fetchAllDistricts() {
        fetch('api/ChooseUniversity/ChoseSpecDistrictList')
            .then(response => response.json() as Promise<DistrictDTO[]>)
            .then(data => {
                this.setState({
                    districts:
                    data.map<District>(district => new District(district.id, district.name))
                })
            });        
    }
    

    submitFilter(selectValueSubmit, districtValueSubmit) {
        if (selectValueSubmit && districtValueSubmit) {
            let result: number[];
            result = [];
            for (let i = 0; i < selectValueSubmit.length; i++) {
                result.push(selectValueSubmit[i].value)
            }
            let subjectsAndDistrict = { ListSubjects: result, District: districtValueSubmit.value, countOfElementsOnPage:10,page:1 }

            this.fetchData(subjectsAndDistrict);
        }
        else
        {
            alert('Pick out some subjects or select district');
        }
    }

    loadFromServer(selected) {
        let result: number[];
        result = [];
        for (let i = 0; i < this.state.selectValueSubjects.length; i++) {
            result.push(this.state.selectValueSubjects[i].value)
        }

        let subjectsAndDistrict = { ListSubjects: result, District: this.state.selectDistrict.value, countOfElementsOnPage: 10, page: selected+1 }
       
        this.fetchData(subjectsAndDistrict);
    }

    handlePageClick = (data) => {
        let selected = data.selected;
        this.loadFromServer(selected);
    }

    private fetchData(subjectsAndDistrict: object)
    {
        fetch('api/ChooseUniversity/ChoseSpecBySublist', {
            method: 'POST',
            body: JSON.stringify(subjectsAndDistrict),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<SpecialtyInfo>)
            .then(data => {
                this.setState({ univers: data })
            })
    }
         
    public render() {
        
        let tabbord;
        if (this.state.univers.countOfAllElements == 0) {
            tabbord = <div>
                <h1>По даному запиту нічого не знайдено змініть вибрані другі предмети обо область.</h1>
            </div>
        }
        else
        {
            tabbord = <ListSpecialties specialties={this.state.univers.listSpecialties} />
        }

        let pagin;
        if (this.state.univers.countOfAllElements > 10)
        {
            pagin = <ReactPaginate 
                previousLabel={"Попередня"}
                nextLabel={"Наступна"}
                breakLabel={<a>...</a>}
                breakClassName={"break-me"}
                pageCount={this.state.univers.countOfAllElements / 10}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={this.handlePageClick}
                containerClassName={"pagination"}
                subContainerClassName={"pages pagination"}
                activeClassName={"active"} />
        }
        return <div>
            <div className="delete-margin">
                <section className="jumbotron center-block">
                    <div className="container">
                        <div className="navigate">
                            <div className="virtselect col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-1"><p>Предмети</p>
                                <VirtualizedSelect multi={true} options={this.state.subjects} onChange={(valueArray) => this.setState({ selectValueSubjects: valueArray })}
                                    value={this.state.selectValueSubjects}></VirtualizedSelect>
                            </div>
                            <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
                                <VirtualizedSelect multi={false} options={this.state.districts} onChange={(selectDistricty) => this.setState({ selectDistrict: selectDistricty })}
                                    value={this.state.selectDistrict} ></VirtualizedSelect>
                            </div>
                            <div>
                                <button className="col-md-offset-1  col-md-1 col-sm-offset-1 col-sm-2  col-xs-8 col-xs-offset-2 btn btn-primary cus-margin"
                                    onClick={() => this.submitFilter(this.state.selectValueSubjects, this.state.selectDistrict)}> Пошук</button>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div className="container pagination">
                <div className="col-md-10 col-md-offset-1">
                    {tabbord}
                    <div className = "pageBar">
                        {pagin}
                    </div>
                </div>
            </div>
            <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
        </div>
    }
}
