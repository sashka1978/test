import React from 'react'
import Table from '../../Components/Table/Table';
import RatioList from '../../Components/RatioList/RatioList';

interface Props{}
const DesignPage = (props: Props) => {
  return (
    <>
        <h1>Financial Design Page</h1>
        <h2>This is Financial's design page. 
            This is where we will house various desgin aspects of the app
        </h2>
        <RatioList/>
        <Table/>
    </>
  );
};

export default DesignPage
