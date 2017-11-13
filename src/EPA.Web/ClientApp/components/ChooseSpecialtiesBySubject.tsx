import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import ListSpecialties from './ListSpecialties';
import VirtualizedSelect  from 'react-virtualized-select';
import 'react-select/dist/react-select.css';
import 'react-virtualized-select/styles.css';
import 'react-virtualized/styles.css';
import 'isomorphic-fetch';

interface Specialitys {
    subjects: Subject[];
    univers: Univer[];
    districts: District[];
    selectValueSubjects: { label: string, value: number }[];
    selectDistrict: { label: string, value: number };
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
            univers: [],
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
            let subjectsAndDistrict = { ListSubjects: result, District: districtValueSubmit.value }

            fetch('api/ChooseUniversity/ChoseSpecBySublist', {
                method: 'POST',
                body: JSON.stringify(subjectsAndDistrict),
                headers: { 'Content-Type': 'application/json' }
            }).then(response => response.json() as Promise<Univer[]>)
                .then(data => {
                    this.setState({ univers: data })
                })
        }
        else
        {
            alert('Pick out some subjects or select district');
        }
    }
      
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

        return <div>
            <div className="delete-margin">
                <section className="jumbotron center-block">
                    <div className="container">
                        <div className="navigate">
                            <div className="virtselect col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2"><p>Предмети</p>
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
                    <ListSpecialties specialties={this.state.univers} />
                </div>
            </div>
            <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
        </div>
    }
}
