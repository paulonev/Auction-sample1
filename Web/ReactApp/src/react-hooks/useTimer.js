import moment from "moment";
import { useEffect, useState } from "react";

/**
 * 
 * @param { interval of time generation } interval 
 * @returns currentDate in form of Date
 */
export const useTimer = (interval=60000) => {
    const [time, setTime] = useState(moment().toDate());

    useEffect(() => {
        const timer = setInterval(() => {
            setTime(moment().toDate());
        }, interval);

        return () => clearInterval(timer);
    }, [time, interval]);

    return { time };
}