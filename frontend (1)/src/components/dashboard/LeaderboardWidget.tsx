import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { useEmployees } from "@/hooks/useApiData";
import { Trophy } from "lucide-react";

const medals = ["🥇", "🥈", "🥉"];

export default function LeaderboardWidget() {
  const { data: employees = [] } = useEmployees();
  const sorted = [...employees].sort((a, b) => b.totalPoints - a.totalPoints).slice(0, 5);

  return (
    <Card className="glass-card">
      <CardHeader>
        <CardTitle className="text-lg font-semibold flex items-center gap-2">
          <Trophy className="w-5 h-5 text-gold" /> Top Performers
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-3">
        {sorted.length === 0 ? (
          <p className="text-center text-muted-foreground py-4">Loading...</p>
        ) : (
          sorted.map((emp, i) => (
            <div key={emp.id} className="flex items-center gap-3 p-2 rounded-lg hover:bg-muted/50 transition-colors">
              <span className="text-lg w-8 text-center">{i < 3 ? medals[i] : `#${i + 1}`}</span>
              <Avatar className="h-9 w-9">
                <AvatarFallback className="bg-primary/10 text-primary text-xs font-semibold">{emp.avatar}</AvatarFallback>
              </Avatar>
              <div className="flex-1 min-w-0">
                <p className="text-sm font-medium truncate">{emp.name}</p>
                <p className="text-xs text-muted-foreground">{emp.department}</p>
              </div>
              <span className="text-sm font-bold text-gold">{emp.totalPoints.toLocaleString()}</span>
            </div>
          ))
        )}
      </CardContent>
    </Card>
  );
}
