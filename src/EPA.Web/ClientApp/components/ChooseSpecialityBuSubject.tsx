import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import React, { Component } from 'react';
//import { VirtualizedSelect } from 'react-virtualized-select'
import VirtualizedSelect from 'react-virtualized-select'
/*import { View } from 'react-native'
import SelectMultiple from 'react-native-select-multiple'
*/

import 'react-select/dist/react-select.css'
import 'react-virtualized/styles.css'
import 'react-virtualized-select/styles.css'

import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';

interface Specialitys {
    subjects: Subject[];
    univers: Univer[];
    selectValueSub: number[];
    selectDistrict: number[];
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

export class ChooseSpecialityBuSubject extends React.Component<RouteComponentProps<{}>, Specialitys> {

    constructor() { super(); this.state = { subjects: [], univers: [], selectValueSub: [], selectDistrict: [] } }

    componentDidMount() {
        this.fetchDataSubject()
    }

    fetchDataSubject() {
        fetch('api/ChooseUniversity/ChoseSpecBySub')
            .then(response => response.json() as Promise<Subject[]>)
            .then(data => {
                this.setState({ subjects: data });
            });
    }
    

    submitFiltr(selectValueSubmit: number[])
    {
        fetch('api/ChooseUniversity/ChoseSpecBySublist', {
            method: 'POST',
            body: JSON.stringify(selectValueSubmit),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<Univer[]>)
            .then(data => { this.setState({ univers: data }) })
    }
      
    public render() {
        
        let myListDisctict = [{ label: "Всі", value: 0 }, {label:"Львівська",value:1}];
        let myList = [{ label: "11", value: 0 }];  
        myList.pop();
        for (let i = 0; i < this.state.subjects.length; i++)
        {
            myList.push({ label: this.state.subjects[i].name,value: this.state.subjects[i].id })
        }  

        return <div className="col-md-offset-1  col-md-10  col-sm-10  col-xs-10 col-xs-offset-1">
            <div className="navigate">
                <div className="virtselect  col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2"><p>Предмети</p>
                    <VirtualizedSelect multi={true} options={myList} onChange={(selectValueSub) => this.setState({ selectValueSub })}
                    value={this.state.selectValueSub}></VirtualizedSelect>
                </div>
                <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
                    <VirtualizedSelect multi={true} options={myListDisctict} onChange={(selectDistrict) => this.setState({ selectDistrict })}
                    value={this.state.selectDistrict}></VirtualizedSelect>
                </div>
                <button className="col-md-offset-1  col-md-1 col-sm-offset-1 col-sm-2  col-xs-8 col-xs-offset-2 btn btn-primary" onClick={() => this.submitFiltr(this.state.selectValueSub)}> Пошук</button>
            </div>
            <div className="col-md-offset-1  col-md-10 col-sm-offset-1 col-sm-10  col-xs-10 col-xs-offset-1">
                {this.state.univers.map(univer =>
                    <div>Спеціальність: {univer.name} Університет:{univer.university}, Адреса:{univer.address},
                    Сайт:{univer.site},
                        Предмети:{univer.subjects.map(sub => sub.name)}
                    </div> 
                    )}
            </div>
        </div>
    }
}
