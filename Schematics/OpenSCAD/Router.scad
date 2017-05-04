module router()
{
// _device();
// first: opened, second: angle?
_device();
antenna(0,0);
// antenna(1,0);
// antenna(0,1);
// antenna(0,2);
//antenna(0,3);
}

module _device()
{
    linear_extrude(height=25)
    {
        translate([5,5])
            offset(5)
                square(95);
    }
}

module rj45slot()
{
    rj45points=[[0,0],[12,0],[12,7],[9,7],[9,9],[8,9],[8,10],[4,10],[4,9],[3,9],[3,7],[0,7]];
    linear_extrude(12)polygon(rj45points);
}


module antenna(rotX,rotY)
{
    // rotate([0,0,0])
    translate([20,0,13])
    color("grey")
    {
        rotate([90,0,0])linear_extrude(height=20)circle(d=13); // trzon
        translate([0,-13,0])rotate([rotX*90,0,0])rotate([0,rotY*90,0])
        translate([0,0,-7])
        {
            linear_extrude(height=18)circle(d=13);
            translate([0,0,18])linear_extrude(height=20)circle(d=9);
            translate([0,0,18+20])linear_extrude(height=84)circle(d=5);
        }
    }
}