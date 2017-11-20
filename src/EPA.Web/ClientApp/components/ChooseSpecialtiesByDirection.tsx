import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import ListSpecialties from './ListSpecialties';
import ReactPaginate from 'react-paginate';
import VirtualizedSelect from 'react-virtualized-select';
import 'react-select/dist/react-select.css';
import 'react-virtualized-select/styles.css';
import 'react-virtualized/styles.css';
import 'isomorphic-fetch';


interface Specialties {
    directions: GeneralDirection[];
    selectValueDirection: { label: string, value: number };
    districts: District[];
    selectDistrict: { label: string, value: number };
    univers: SpecialtyInfo;
    districtId: number;
    directionId: number;
}

interface GeneralDirectionDTO {
    name: string;
    id: number;
}

class GeneralDirection {
    value: number;
    label: string;

    constructor(value: number, label: string) {
        this.value = value;
        this.label = label;
    }
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

interface SpecialtyInfo {
    list: Univer[];
    count: number;
}



export class ChooseSpecialtiesByDirection extends React.Component<RouteComponentProps<{}>, Specialties>
{
    constructor() {
        
        super();
        var countOfElementsOnPage = 10;
        this.state = {
            directions: [],
            selectValueDirection: { value: 0, label: "Всі" },
            univers: { list: [], count: 1 },
            districts: [],
            selectDistrict: { value: 0, label: "Всі" },
            districtId: 0,
            directionId: 0
        }
        
    }

    componentDidMount() {
        this.fetchDataDirections();
        this.fetchAllDistricts();
    }

    fetchDataDirections() {
        fetch('api/ChooseSpecialties/getdirection')
            .then(response => response.json() as Promise<GeneralDirectionDTO[]>)
            .then(data => {
                this.setState({
                    directions:
                    data.map<GeneralDirection>(direction => new GeneralDirection(direction.id, direction.name))
                });
            });
    }

    fetchAllDistricts() {
        fetch('api/ChooseSpecialties/ChoseSpecDistrictList')
            .then(response => response.json() as Promise<DistrictDTO[]>)
            .then(data => {
                this.setState({
                    districts:
                    data.map<District>(district => new District(district.id, district.name))
                })
            });  
    }

    handlePageClick = (data) => {
        let selected = data.selected;
        let directionAndDistrict = { GeneralDirection: this.state.directionId, District: this.state.districtId, countOfElementsOnPage: 10, page: selected }
        this.fetchData(directionAndDistrict);
    }

    private fetchData(directionAndDistrict) {
        fetch('api/ChooseSpecialties/bydirection', {
            method: 'POST',
            body: JSON.stringify(directionAndDistrict),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<SpecialtyInfo>)
            .then(data => {
                this.setState({ univers: data })
            })
    }

    submitFilter(selectValueSubmit, districtValueSubmit) {
        if (selectValueSubmit && districtValueSubmit)
        {
            let directionAndDistrict = { GeneralDirection: selectValueSubmit.value, District: districtValueSubmit.value, countOfElementsOnPage: 10, page: 0 }

            this.fetchData(directionAndDistrict); 

            this.setState({ districtId: districtValueSubmit.value, directionId: selectValueSubmit.value });
        }
        else {
            alert('Pick out direction or select district');
        }
    }


    render() {

        let tabbord;
        if (this.state.univers.count == 0) {
            tabbord = <div>
                <h1>По даному запиту нічого не знайдено змініть вибрані галузь або область.</h1>
            </div>
        }
        else {
            tabbord = <ListSpecialties specialties={this.state.univers.list} />
        }

        let pagin;
        if (this.state.univers.count > 10) {
            pagin = <ReactPaginate
                previousLabel={"Попередня"}
                nextLabel={"Наступна"}
                breakLabel={<a>...</a>}
                breakClassName={"break-me"}
                pageCount={this.state.univers.count / 10}
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
                               <div className="virtselect  col-md-3 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2"><p>Галузі</p>
                                   <VirtualizedSelect multi={false} options={this.state.directions} onChange={(value) => this.setState(
                                    { selectValueDirection: value })
                                }
                                    value={this.state.selectValueDirection}></VirtualizedSelect>
                            </div>
                               <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
                                   <VirtualizedSelect multi={false} options={this.state.districts} onChange={(selectDistricty) => this.setState(
                                    { selectDistrict: selectDistricty })
                                }
                                    value={this.state.selectDistrict} ></VirtualizedSelect>
                            </div>
                            <button className="col-md-offset-1  col-md-2 col-sm-offset-1 col-sm-2  col-xs-8 col-xs-offset-2 btn btn-primary cus-margin"
                                onClick={() => this.submitFilter(this.state.selectValueDirection, this.state.selectDistrict)}> Пошук</button>
                        </div>
                    </div>
                </section>
            </div>
            <div className="container">
                <div className="col-md-10 col-md-offset-1">
                       {tabbord}
                       <div className="pageBar">
                           {pagin}
                       </div>
                </div>
            </div>
            <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
        </div>
    }
}