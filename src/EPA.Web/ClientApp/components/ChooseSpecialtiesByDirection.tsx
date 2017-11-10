import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import React, { Component } from 'react';
//import { VirtualizedSelect } from 'react-virtualized-select'
import VirtualizedSelect from 'react-virtualized-select'
/*import { View } from 'react-native'
import SelectMultiple from 'react-native-select-multiple'
*/

import ListSpecialties from './ListSpecialties'

import List from './List'



import 'react-select/dist/react-select.css'
import 'react-virtualized/styles.css'
import 'react-virtualized-select/styles.css'

import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';

import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion'



interface Specialties {
    directions: GeneralDirection[];
    selectValueDir: { label: string, value: number }[];
    districts: District[];
    selectDistrict: { label: string, value: number };
    univers: Univer[];
}

interface GeneralDirection {
    name: string;
    id: number;
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



export class ChooseSpecialtiesByDirection extends React.Component<RouteComponentProps<{}>, Specialties>
{
    constructor() {
        super();
        this.state = {
            directions: [],
            selectValueDir: [],
            univers: [],
            districts: [],
            selectDistrict: { value: 0, label: "Всі" }
        }
    }

    componentDidMount() {
        this.fetchDataSubject();
    }

    fetchDataSubject() {
        fetch('api/choosespeciality/getdirection')
            .then(response => response.json() as Promise<GeneralDirection[]>)
            .then(data => {
                this.setState({ directions: data });
            });
    }

    render() {
        let myList = [{ label: "13", value: 0 }];
        myList.pop();
        for (let i = 0; i < this.state.directions.length; i++)
        {
            myList.push({ label: this.state.directions[i].name, value: this.state.directions[i].id })
        }

        let myListDisctict = [{ label: "Всі", value: 0 }];
        for (let i = 0; i < this.state.districts.length; i++)
        {
                myListDisctict.push({ label: this.state.districts[i].name, value: this.state.districts[i].id })
            }

        return <div className="col-md-offset-1  col-md-10  col-sm-10  col-xs-10 col-xs-offset-1 pagin">
            <div className="navigate">
                <div className="virtselect  col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2 pagin"><p>Галузі</p>
                    <VirtualizedSelect multi={true} options={myList} onChange={(valueArray) => this.setState({ selectValueDir: valueArray })}
                        value={this.state.selectValueDir}></VirtualizedSelect>
                </div>

                <div>
                    <ListSpecialties univers={this.state.univers} />
                </div>

            </div>
        </div>
    
    }

}
