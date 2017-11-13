import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import  VirtualizedSelect  from 'react-virtualized-select'
import ListSpecialties from './ListSpecialties'
import 'react-virtualized/styles.css'
import 'react-select/dist/react-select.css'
import 'isomorphic-fetch';
import { ReactPaginate } from 'react-paginate';

interface Specialitys {
    subjects: Subject[];
    univers: SpecialitiInfo;
    districts: District[];
    selectValueSubjects: { label: string, value: number }[];
    selectDistrict: { label: string, value: number };
}


interface SpecialitiInfo {
    univer: Univer[];
    countOfAllElements: number;
    page: number;
}

interface District {
    name: string;
    id: number;
}

interface Subject {
    id: number;
    name: string
}

interface Univer {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
}

export class ChooseSpecialtiesBySubject extends React.Component<RouteComponentProps<{}>, Specialitys> {

    constructor()
    {
        super();
        this.state = {
            subjects: [],
            univers: { univer: [], countOfAllElements:0, page:0 },
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
            .then(response => response.json() as Promise<Subject[]>)
            .then(data => {
                this.setState({ subjects: data });
            });
    }
    fetchAllDistricts() {
        fetch('api/ChooseUniversity/ChoseSpecDistrictList')
            .then(response => response.json() as Promise<District[]>)
            .then(data => { this.setState({ districts: data }) })
    }
    

    submitFilter(selectValueSubmit, districtValueSubmit) {
        if (selectValueSubmit != null && selectValueSubmit.length > 0 && districtValueSubmit != undefined) {
            let result: number[];
            result = [];
            for (let i = 0; i < selectValueSubmit.length; i++) {
                result.push(selectValueSubmit[i].value)
            }
            let subjectsAndDistrict = { ListSubjects: result, District: districtValueSubmit.value,countElementsOnPage:10,page:1 }

            fetch('api/ChooseUniversity/ChoseSpecBySublist', {
                method: 'POST',
                body: JSON.stringify(subjectsAndDistrict),
                headers: { 'Content-Type': 'application/json' }
            }).then(response => response.json() as Promise<SpecialitiInfo>)
                .then(data => {
                    this.setState({ univers: data })
                })
        }
        else
        {
            alert('Pick out some subjects or select district');
        }
    }

    loadCommentsFromServer(selected) {
        let result: number[];
        result = [];
        for (let i = 0; i < this.state.selectValueSubjects.length; i++) {
            result.push(this.state.selectValueSubjects[i].value)
        }
        let subjectsAndDistrict = { ListSubjects: result, District: this.state.selectDistrict.value, countElementsOnPage: 10, page: selected }
        fetch('api/ChooseUniversity/ChoseSpecBySublist', {
            method: 'POST',
            body: JSON.stringify(subjectsAndDistrict),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.json() as Promise<SpecialitiInfo>)
            .then(data => {
                this.setState({ univers: data });
            });
    }

    handlePageClick = (data) => {
        let selected = data.selected;
        //let offset = Math.ceil(selected * this.state.perPage);
        this.loadCommentsFromServer(selected);
    };
    
      
    public render() {
        
        let myListDisctict = [{ label: "Всі", value: 0 }];
        for (let i = 0; i < this.state.districts.length; i++) {
            myListDisctict.push({ label: this.state.districts[i].name, value: this.state.districts[i].id })
        }
        let myList = [{ label: "11", value: 0 }];  
        myList.pop();
        for (let i = 0; i < this.state.subjects.length; i++)
        {
            myList.push({ label: this.state.subjects[i].name,value: this.state.subjects[i].id })
        }
        let content;
        if (this.state.univers.page != 0)
            content = <div><ListSpecialties univers={this.state.univers.univer} />
            {/*<ReactPaginate className="qqq"
                previousLabel={"Попередня"}
                nextLabel={"Наступна"}
                breakLabel={<a href="">...</a>}
                breakClassName={"break-me"}
                pageCount={this.state.univers.countOfAllElements / 10}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={this.handlePageClick}
                containerClassName={"pagination"}
                subContainerClassName={"pages pagination"}
                activeClassName={"active"} />*/}</div>

        return <div>
            <div className="delete-margin">
                <section className="jumbotron center-block">
                    <div className="container">
                        <div className="navigate">
                            <div className="virtselect col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-1"><p>Предмети</p>
                                <VirtualizedSelect multi={true} options={myList} onChange={(valueArray) => this.setState({ selectValueSubjects: valueArray })}
                                    value={this.state.selectValueSubjects}></VirtualizedSelect>
                            </div>
                            <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
                                <VirtualizedSelect multi={false} options={myListDisctict} onChange={(selectDistricty) => this.setState({ selectDistrict: selectDistricty })}
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
            <div className="container">
                <div className="col-md-10 col-md-offset-1">
                    {content}
                </div>
            </div>
            <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
        </div>
    }
}
