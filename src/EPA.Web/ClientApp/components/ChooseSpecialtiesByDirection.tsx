import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import VirtualizedSelect from 'react-virtualized-select'

interface Specialties
{
    directions: GeneralDirection[];
    selectValueSub: { value: number, label: string };
}

interface GeneralDirection
{
    name: string;
    id: number;
}

export class ChooseSpecialtiesByDirection extends React.Component<RouteComponentProps<{}>, Specialties>
{
    constructor()
    {
        super();
        this.state = { directions: [], selectValueSub: { value: 1, label: "" } }
       // this.handleChange = this.handleChange.bind(this);
    }

    //handleChange(event) {
    //    this.setState({ value: event.target.value });
    //}

    fetchDataSubject() {
        fetch('api/ChooseUniversity/getdirection')
            .then(response => response.json() as Promise<GeneralDirection[]>)
            .then(data => {
                this.setState({ directions: data });
            });
    }

    render()
    {
        let myList = [{ label: "11", value: 0 }];
        myList.pop();
        for (let i = 0; i < this.state.directions.length; i++) {
            myList.push({ label: this.state.directions[i].name, value: this.state.directions[i].id })
        }  

        return <div className="col-md-offset-1  col-md-10  col-sm-10  col-xs-10 col-xs-offset-1">
            <p>Hello word</p>
            <div className="virtselect  col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2"><p>Предмети</p>
                <VirtualizedSelect multi={true} options={myList} onChange={(selectValueSub) => this.setState({ selectValueSub })}
                    value={this.state.selectValueSub}></VirtualizedSelect>
            </div>
        </div>
    }
    
}
