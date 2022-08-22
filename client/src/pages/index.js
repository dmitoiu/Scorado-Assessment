import React from "react";
import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Home.module.css'
import {useEffect, useState} from "react";
import VariableIcon from "@heroicons/react/outline/VariableIcon";

export default function Home() {

  let [board, setBoard] = useState(Array.from({length: 9},()=> Array.from({length: 9}, () => 0)));

  let solveBoard = () => {
      let solvedBoard = fetch("http://localhost:5208/api/SudokuSolver", {
          method: "POST",
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json',
          },
          body: JSON.stringify(board)
      }).then(response => response.json()).then(data => setBoard(data));
      console.log("Solved Board: ", board);
  }

  let updateBoard = (row, column, event) => {
      let copy = [...board];
      copy[row][column] = +event.target.value;
      setBoard(copy);
      console.log("Board: ", board);
  }

  let resetBoard = (e) => {
      let reset = Array.from({length: 9},()=> Array.from({length: 9}, () => 0));
      setBoard(reset);
  }

  useEffect(() => {
      fetch("http://localhost:5208/api/SudokuSolver");
  }, []);

  return (
      <div className={"min-h-full"}>
          <h1 className="text-3xl text-white py-5 px-10 font-bold">
              Sudoku Solver
          </h1>
          <div className="relative px-4 sm:px-6 lg:px-8 bg-gray-800" style={{height: 580}}>
              <div className="mt-8 flex flex-col min-h-full">
                  <div className="-my-2 -mx-4 overflow-x-auto sm:-mx-6 lg:-mx-8" style={{height: 600}}>
                      <div className="flex flex-row min-w-full py-2 align-middle md:px-6 lg:px-8">
                          <table>
                              <tbody>
                              {board.length > 0 && board.map((row, rowIndex) => (
                                  <tr key={rowIndex}>
                                      {row.map((column, columnIndex) => (
                                          <td key={columnIndex}>
                                              <div style={{maxWidth: 50}}>
                                                  <input
                                                      type={"email"}
                                                      name={[rowIndex][columnIndex]}
                                                      value={board[rowIndex][columnIndex]}
                                                      style={{width: 50}}
                                                      onChange={e => updateBoard(rowIndex, columnIndex, e)}
                                                      className="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
                                                      placeholder=""
                                                  />
                                              </div>
                                          </td>
                                      ))}
                                  </tr>
                              ))}
                              </tbody>
                          </table>
                      </div>
                      <span className="px-2 sm:ml-5">
                          <button
                              type="button"
                              onClick={solveBoard}
                              className="inline-flex w-250 items-center px-8 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-500 hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-indigo-500"
                          >
                            <VariableIcon className="-ml-1 mr-2 h-5 w-5" aria-hidden="true" />
                            solve
                          </button>
                      </span>
                      <span>
                          <button
                              type="button"
                              onClick={resetBoard}
                              className="inline-flex w-250 items-center px-8 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-500 hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-indigo-500"
                          >
                            <VariableIcon className="-ml-1 mr-2 h-5 w-5" aria-hidden="true" />
                            Reset
                          </button>
                      </span>
                  </div>
              </div>
          </div>
      </div>
  )
}
