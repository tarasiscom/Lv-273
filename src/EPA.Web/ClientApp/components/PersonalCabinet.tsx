import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ErrorHandlerProp, ResponseChecker } from './App';
import { Loading } from './Loading';

interface StateTypes {
    user: User;
    specialties: Specialty[];
    loading: boolean;
}

interface User {
    name: string;
    surname: string;
    district: string;
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

export class PersonalCabinet extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {

    constructor() {
        super();
        this.state = {
            loading: true,
            specialties: [],
            user: { name: "", surname: "", district: "" }
            
        }
    }

    componentDidMount() {

    }

    private fetchFavotiteSpecialties()
    {
        fetch('api/User/FavoriteSpecialties')
            .then(Response => ResponseChecker<Specialty[]>(Response, this.props.onError))
    }

    render()
    {
        if (this.state.loading) {
            return <Loading />
        }
        else {
            return <div>USER INFO bla bla bla</div>
        }
        
    }
}