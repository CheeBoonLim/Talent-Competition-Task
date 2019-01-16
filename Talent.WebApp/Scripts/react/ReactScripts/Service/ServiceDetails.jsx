import React from 'react';
import CommentDetail from './CommentDetails.jsx';
import OrderDetail from './OrderDetail.jsx';
import ProductDetail from './ProductDetail.jsx';
import SellerDetail from './SellerDetails.jsx';
import SellerRating from './SellerRating.jsx';
import ServiceSectionDetail from './ServiceSectionDetail.jsx';
import ServiceNavigation from './ServiceNavigation.jsx'

export default class ServiceDetails extends React.Component {
    render() {
        return (
            <React.Fragment>
                <ServiceNavigation />
                <div className="ui container">
                    <div className="ui grid service-details">
                        <ProductDetail />
                        <OrderDetail />
                        <ServiceSectionDetail />
                        <SellerDetail />
                        <SellerRating />
                        <CommentDetail />
                    </div>
                </div>
            </React.Fragment >
        )
    }
}