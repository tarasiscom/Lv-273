import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
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

interface Subject {
    id: number;
    name: string
}

export class FavoriteSpecialties extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {

    constructor() {
        super();
        this.state = {
            count: { allElements: 1, forOnePage: 1 },
            loading: true,
            specialties: [],
        }
    }

    componentDidMount() {
        this.fetchCountOfSpecialties();
        this.fetchFavoriteSpecialties(0);
    }

    private fetchCountOfSpecialties() {
        let path = 'api/User/GetSpecialtiesCount';

        GetFetch<any>(path)
            .then(data => {
                this.setState(
                    {
                        count: data
                    })
            }).catch(er => this.props.onError(er))
    }

    private fetchFavoriteSpecialties(page) {
        let path = 'api/User/FavoriteSpecialties';

        PostFetch<any>(path, page)
            .then(data => {
                this.setState({
                    specialties: data,
                    loading: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    render() {

        if (this.state.loading) {
            return <Loading />
        }
        else {
            return <div className="  text-font-favorite-sp">
                {this.renderSpecialtiesList()}
            </div>

        }
    }

    private renderSpecialtiesList() {
        let tabbord;
        let header;

        header = <div>
            <div className="jumbotron ">
                <div className="container">
                    <h1 className="display-3">Обрані спеціальності</h1>
                    <p className="lead"></p>
                </div>
            </div>
        </div>

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

        return <div>
            {header}
            <div className="container pad-for-footer caption col-md-12 col-lg-12 col-sm-12 col-xs-12">
                <div className="col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2 col-sm-12 col-xs-12">
                    {tabbord}
                    <div className="pageBar">
                        {pagin}
                    </div>
                </div>
        </div>
        </div>
            }

    private handlePageClick = (data) => {

                let selected = data.selected;
        this.fetchFavoriteSpecialties(selected);
    }
}