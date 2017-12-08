import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import ListSpecialties from './ListSpecialties';
import { Loading } from './Loading';
import ReactPaginate from 'react-paginate';
import VirtualizedSelect from 'react-virtualized-select';
import 'react-select/dist/react-select.css';
import 'react-virtualized-select/styles.css';
import 'react-virtualized/styles.css';
import 'isomorphic-fetch';

import { ErrorHandlerProp, GetFetch, PostFetch } from './App';

interface Specialties {
    directions: GeneralDirection[];
    selectValueDirection: { label: string, value: number };
    districts: District[];
    selectDistrict: { label: string, value: number };
    specialties: Specialty[];
    districtId: number;
    directionId: number;
    count: Count;
    loading: boolean;
    loadingDirectionsAndDistricts: boolean;
    isSubmitted: boolean;
}

interface Count {
   
    allElements: number;
    forOnePage: number;
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

interface Specialty {
    id: number;
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
    isFavorite:boolean
}


export class ChooseSpecialtiesByDirection extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, Specialties>
{
    constructor() {
        
        super();
        this.state = {
            directions: [],
            selectValueDirection: { value: 0, label: "Виберіть галузь" },
            specialties: [],
            districts: [],
            selectDistrict: { value: 0, label: "Всі" },
            districtId: 0,
            directionId: 0,
            count: { allElements: 1, forOnePage: 1 },
            loading: true,
            loadingDirectionsAndDistricts: true,
            isSubmitted: false
        }
        
    }
    
    render() {
        if (this.state.loadingDirectionsAndDistricts) {
            return <Loading />
        }
        else {
            return <div className="pad-for-footer">
                <div className="delete-margin">
                    <section className="jumbotron center-block">
                        <div className="container">
                            <div className="navigate">
                                <div className="virtselect  col-md-3 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2"><p>Галузі</p>
                                    <VirtualizedSelect multi={false}
                                        options={this.state.directions}
                                        onChange={this.handleOnChangeDirection}
                                        value={this.state.selectValueDirection}>
                                    </VirtualizedSelect>
                                </div>
                                <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
                                    <VirtualizedSelect multi={false}
                                        options={this.state.districts}
                                        onChange={this.handleOnChangeDistrict}
                                        value={this.state.selectDistrict} >
                                    </VirtualizedSelect>
                                </div>
                                <button className="col-md-offset-1  col-md-2 col-sm-offset-1 col-sm-2  col-xs-8 col-xs-offset-2 btn btn-primary cus-margin"
                                    onClick={this.handleOnClick}> Пошук
                               </button>
                            </div>
                        </div>
                    </section>
                </div>
                {
                    this.state.isSubmitted ?
                        this.renderSubmitted() :
                        <div></div>
                }
                <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
            </div>
        }
    }

    private renderListSpecialies() {
        let pagination;
        if (this.state.count.allElements > 10) {
            pagination = <ReactPaginate
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
        let itemSpecialty;
        if (this.state.count.allElements == 0) {
            itemSpecialty = <div>
                <h1>По даному запиту нічого не знайдено змініть вибрані галузь або область.</h1>
            </div>
        }
        else {
            itemSpecialty = <ListSpecialties specialties={this.state.specialties} />
        }
        return <div className="container">
            <div className="col-md-10 col-md-offset-1">
                {itemSpecialty}
                <div className="pageBar">
                    {pagination}
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
        this.fetchAllDirections();
        this.fetchAllDistricts();
    }


    private fetchAllDirections() {
        GetFetch<GeneralDirectionDTO[]>('api/ChooseSpecialties/directionsList')                 
            .then(data => {
                this.setState({
                    directions: data.map<GeneralDirection>(direction => new GeneralDirection(direction.id, direction.name)),
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

    handleOnChangeDirection = (value) => {
        this.setState({ selectValueDirection: value })
    }

    handleOnChangeDistrict = (selectDistricty) => {
        this.setState({ selectDistrict: selectDistricty })
    }

    handleOnClick = () => {
        this.submitFilter(this.state.selectValueDirection, this.state.selectDistrict)
    }    

    handlePageClick = (data) => {
        let selected = data.selected;
        let directionAndDistrict = {
            GeneralDirection: this.state.directionId,
            District: this.state.districtId,
            countOfElementsOnPage: this.state.count.forOnePage,
            page: selected
        }
        this.fetchData(directionAndDistrict);
    }

    private fetchData(directionAndDistrict) {
        GetFetch<any>('api/ChooseSpecialties/byDirectionAndDistrict/' + directionAndDistrict.GeneralDirection + '/' + directionAndDistrict.District + '/' + directionAndDistrict.page)
            .then(data => {
                this.setState({
                    specialties: data,
                    loading: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    submitFilter(selectValueSubmit, districtValueSubmit) {
        if (selectValueSubmit && districtValueSubmit) {
            this.setState({
                isSubmitted: false,
                loading: true
            });
            let directionAndDistrict = {
                GeneralDirection: selectValueSubmit.value,
                District: districtValueSubmit.value,
                page: 0
            }

            GetFetch<any>('api/ChooseSpecialties/count/' + directionAndDistrict.GeneralDirection + '/' + directionAndDistrict.District + '/')
                .then(data => {
                    this.setState({
                        count: data,
                        isSubmitted: true
                    })
                })
                .catch(er => this.props.onError(er))

            this.fetchData(directionAndDistrict);
            this.setState({
                districtId: districtValueSubmit.value,
                directionId: selectValueSubmit.value
            });
        }
        else {
            alert('Pick out direction or select district');
        }
    } 

}

