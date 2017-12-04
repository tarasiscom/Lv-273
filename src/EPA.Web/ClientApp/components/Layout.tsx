import * as React from 'react';
import { NavigationMenu } from './NavigationMenu';
import { Footer } from './Footer';

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return <div className='container-fluid remove-all-padding'>
            <NavigationMenu />
            
                {this.props.children}
            
            <Footer />
        </div>;
    }
}
