import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, GetFetch } from './App';
import { Loading } from './Loading';
import ListSpecialties from './ListSpecialties';
import ReactPaginate from 'react-paginate';

interface StateTypes {
    count: Count;
    specialties: Specialty[];
    loading: boolean;
}

interface Count {
    allElements: number;
    forOnePage: number;
}

interface Specialty
{
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
}

interface Subject {
    id: number;
    name: string
}

export class FavoriteSpecialties extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {

    constructor() {
        super();
        this.state = {
            count: { allElements : 1, forOnePage : 1 },
            loading: true,
            specialties: [],
        }
    }

    componentDidMount() {
        this.fetchFavoriteSpecialties();
    }

    private fetchFavoriteSpecialties()
    {
        let path = 'api/User/FavoriteSpecialties'

        GetFetch<any>(path)
            .then(data => {
                this.setState({
                    specialties: data,
                    loading: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    render()
    {

        if (this.state.loading) {
            return <Loading />
        }
        else {
            return <div className="container caption text-font-favorite-sp">
                {this.renderSpecialtiesList()}
                </div>
        }   
    }

    private renderSpecialtiesList()
    {
        let tabbord;

        if (this.state.count.allElements == 0) {
            tabbord = <div>
                <h1 className="uni-recom"> Ви не вподобали жодної спеціальності</h1>
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

    private renderHeader()
    {
        
    }
}