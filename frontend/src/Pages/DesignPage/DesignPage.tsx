import React from 'react'
import Table from '../../Components/Table/Table';
import RatioList from '../../Components/RatioList/RatioList';
import { testIncomeStatementData } from '../../Components/Table/testData';


interface Props{}
const tableConfig = [
  {
    label: "Market Cap",
    render: (company: any) => company.marketCapTTM,
    subTitle: "Total value of all a company's shares of stock",
  },
];
const DesignPage = (props: Props) => {
  return (
    <>
        <h1>Financial Design Page</h1>
        <h2>This is Financial's design page. 
            This is where we will house various desgin aspects of the app
        </h2>
        <RatioList data={testIncomeStatementData} config={tableConfig}/>
        <Table configs={tableConfig} data={testIncomeStatementData}/>
    </>
  );
};

export default DesignPage
