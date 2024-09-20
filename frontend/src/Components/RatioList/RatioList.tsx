import React from 'react'
import { TestDataCompany } from '../Table/testData'

interface Props{}

const data = TestDataCompany[0];

type Company = typeof data;

const config = [
    {
        label: "Company Name",
        render: (comany:Company) => comany.companyName,
        subTitle: "This is the company name",
    },
    {
        label: "Company Name",
        render: (comany:Company) => comany.companyName,
        subTitle: "This is the company name",
    },
];

const RatioList = (props:Props) => {
    const renderedRows = config.map(rows => {
        return (
            <li className="py-3 sm:py-4">
               <div className="flex.items-center.space-x-4">
                    <div className="flex-1 min-w-0">
                        <p className="text-sm font-medium text-gray-900 truncate">
                            {rows.label}
                        </p>
                        <p className="text-sm text-gray-500 truncate">
                            {rows.subTitle && rows.subTitle}
                        </p>
                    </div>  
                     <div className="inline-flex.items-center.text-base.font-semibold.text-gray-900">
                        {rows.render(data)}
                     </div>
                </div>    
            </li>
        )
    })
  return (
    <div className="bg-white shadow rounded-lg mb-4 p-4 sm:p-6 h-full"> 
        <ul className="divide-y divided-gray-200">
            {renderedRows}
        </ul>
    </div>
  )
}

export default RatioList
