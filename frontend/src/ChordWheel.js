import React, { useEffect, useState } from "react";

const ChordWheel = () => {
    const [chordData, setChordData] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch("http://localhost:8080/chord")
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch chord data")
                }
                return response.json();
            })
            .then((data) => setChordData(data))
            .catch((error) => {
                console.error("Error fetching chord data:", error);
                setError(error);
            });
    }, []);

    if (error) {
        return <div>Error: {error.message}</div>
    }

    return (
        <div>
            {chordData ? ( // Check if chordData is not null or undefined
                <>
                    <h2>Chord Wheel - {chordData.chord}</h2>
                    <p>Available Chords: {chordData.chords.join(", ")}</p>
                    <div
                        style={{
                            width: "200px",
                            height: "200px",
                            borderRadius: "50%",
                            backgroundColor: "#f0f0f0",
                            position: "relative",
                        }}
                    >
                        {/* Render color wheel sections based on the length of Chords */}
                        {chordData.chords.map((chord, index) => (
                            <div
                                key={index}
                                style={{
                                    width: "200px",
                                    height: "200px",
                                    borderRadius: "50%",
                                    backgroundColor:
                                        chordData.chord === chord
                                            ? "#FF0000" // Change the color of the highlighted section
                                            : "#FFFFFF", // Default color for other sections
                                    position: "absolute",
                                    clipPath: `polygon(
                50% 0%,
                50% 50%,
                ${index === chordData.chords.length - 1 ? "100%" : "calc(50% + 1px)"} 50%,
                ${index === chordData.chords.length - 1 ? "100%" : "calc(50% + 1px)"} 100%,
                50% 100%,
                50% 50%,
                0% 50%,
                0% 0%
              )`,
                                }}
                            ></div>
                        ))}
                    </div>
                </>
            ) : (
                <p>Loading...</p> // Display a loading message while fetching data
            )}
        </div>

    );
};

export default ChordWheel;
